using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Inspiration
-Brackeys ( https://youtu.be/aLpixrPvlB8 )
*/

//private int ScreenWidth = 16;
//private int ScreenHeight = 9;

public class CameraManager : MonoBehaviour
{
	//referenced classes
	Camera mainCamera;
	public ListOfActiveCars activeCars; //connect in hirachy
	private PlacementManager placementManager;
	private SmoothMovementCR lerpCR;
	private CameraShaker camShake;

	public List<Transform> targets;


	public Vector3 offset;

	public float smoothTime = .5f;
	private Vector3 velocity;
	public float zoomLimiter = 30f;
	public float minZoom = 30f;
	public float maxZoom = 15f;

	[SerializeField]
	float favorFirstPlaced = 0.6f;
	[SerializeField]
	float secondsToPanCam = 1f;

	//global variables for Lipo in Coroutine (short: CR)
	Vector3 lastSmoothPos;
	Vector3 center;
	bool CRisRunning = false;

	//flags
	bool camShakeAllowed = false;


    private void Awake()
    {
		
    }

    // Start is called before the first frame update
    void Start()
	{
		mainCamera = GetComponent<Camera>();
		placementManager = GameObject.Find("/PlacementManager").GetComponent<PlacementManager>();
		lerpCR = GetComponent<SmoothMovementCR>();
		camShake = GetComponent<CameraShaker>();

		CarDestroyer.OnOutOfScreenDestroy += allowCamShake;

		makeTargetsList();

		//these two initialize the camera position
		instantMove(); //functions without smooth movement to bring the camera instantly to the desired location
		instantZoom();

		center = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		//version von Marv
		//GameObject firstPlaced = placementManager.getFirstPlaced();
		//transform.position = new Vector3(firstPlaced.transform.position.x, firstPlaced.transform.position.y, 0);
	}

	void LateUpdate()
	{
		UpdateTargetsList();
		if (targets.Count == 0)
			return;

		move();
		zoom();
		shake();
		
	}

	void shake()
    {
		if (camShake != null)
        {
			if (camShakeAllowed)
			{
				camShakeAllowed = false;
				camShake.startShake(.8f, 2f);
			}
			transform.position += camShake.getShakeValue();
		}
		
	}
	
	void move()
	{
		Vector3 betweenPos = dynamicMovementBetweenCars();
		Vector3 centerPoint = calcCenterPoint();
		center = Vector3.SmoothDamp(center, centerPoint, ref velocity, smoothTime); //update the center Point for smooth Cam movement towards middle point between all cars

		Vector3 lipoCenter = Vector3.Lerp(center, betweenPos, favorFirstPlaced); //final weighted position factoring in center between all cars and the first placed cars

		transform.position = lipoCenter;


		//transform.position = center; //(for testing) gives only the center between all cars 
		//transform.position = betweenPos; //(for testing) gives only the camPos between first and second place 
	}

	void instantMove()
	{
		Vector3 betweenPos = dynamicMovementBetweenCars(); //is instant as long as no one overtakes
		Vector3 centerPoint = calcCenterPoint(); //always instant

		Vector3 lipoCenter = Vector3.Lerp(centerPoint, betweenPos, favorFirstPlaced); //final weighted position factoring in center between all cars and the first placed cars

		transform.position = lipoCenter;


		//transform.position = center; //(for testing) gives only the center between all cars 
		//transform.position = betweenPos; //(for testing) gives only the camPos between first and second place 
	}

	//methods related to the dynamic focus change between cars
	Vector3 dynamicMovementBetweenCars()
    {
		Vector3 firstPlacedPos = tryGetFirstPlacedPos();
			

		if (placementManager.isOvertaken)
		{
			Vector3 prevFirstPos = placementManager.getPreviousFirstPlaced().transform.position;

			StopAllCoroutines(); //stops a coroutine from ever setting CRisRunning to false while another CR is running
			if (CRisRunning == false) //if this value is true the CR must have been stopped prematurely. To keep smooth movement the start point needs to be different
			{
				StartCoroutine(lerpCR.LerpInCenterSpace(placementManager.getPreviousFirstPlaced(), placementManager.getFirstPlaced(), secondsToPanCam));
			}
			else
			{
				//Debug.Log("does this ever get called?");
				StartCoroutine(lerpCR.LerpInCenterSpace(lastSmoothPos, placementManager.getFirstPlaced(), secondsToPanCam));
			}
		}

		return BetweenPos(firstPlacedPos); //function returns either Lerp result or Pos of firstPlaced car
	}

	Vector3 BetweenPos(Vector3 _firstPlacedPos)
	{
		if (CRisRunning) //in andere Funktion auslagern mit Rückgabewert und dort betweenPos initiieren und ausgeben. Einschließlich Deklaration da oben!
		{
			//Debug.Log("Coroutine is running");
			return lastSmoothPos; //value that is calculated by Lerp
		}
		else
		{
			//Debug.Log("Coroutine is not running");
			//Debug.Log("CRisRunning is now false!");
			return _firstPlacedPos;
		}
	}


	//from here on everything that is not Coroutine Lipo related

	Vector3 calcCenterPoint()
	{
		if (targets.Count == 1)
		{
			return targets[0].position;
		}

		var bounds = new Bounds(targets[0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++)
		{
			bounds.Encapsulate(targets[i].position);
		}
		return bounds.center;
	}	

	void UpdateTargetsList() //only checks for empty values in List and deletes them. Might be enough
	{
		targets.RemoveAll(Transform => Transform == null);
	}

	void makeTargetsList()
	{
		foreach(GameObject car in activeCars.carsList) //make this a MonoBehaviour instead of SO
		{
			targets.Add(car.transform);
		}
	}

	Vector3 tryGetFirstPlacedPos() //if there is not yet a first placed defined, i just define it myself! (returns first active car instead)
    {
		if (placementManager.getFirstPlaced() != null)
		{
			return placementManager.getFirstPlaced().transform.position;
		}
		else return activeCars.getCarFromList(0).transform.position;

	}

	void zoom()
	{
		//Debug.Log(getGreatestDistance());
		float newZoom = Mathf.Lerp(maxZoom, minZoom, getGreatestDistance() / zoomLimiter);
		mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newZoom, Time.deltaTime);
	}

	void instantZoom()
	{
		//Debug.Log(getGreatestDistance());
		float newZoom = Mathf.Lerp(maxZoom, minZoom, getGreatestDistance() / zoomLimiter);
		mainCamera.orthographicSize = newZoom;
	}

	float getGreatestDistance()
	{
		var bounds = new Bounds(targets[0].position, Vector3.zero);
		for (int i = 0; i < targets.Count; i++)
		{
			bounds.Encapsulate(targets[i].position);
		}

		//hier das größere nehmen, x oder y Achse!

		float xDistance = bounds.size.x;
		float yDistance = bounds.size.y;

		if (xDistance > yDistance)
		{
			return xDistance;
		}
		else if (yDistance > xDistance)
		{
			return yDistance;
		}
		else
		{
			return xDistance;
		}
	}

	private void allowCamShake(GameObject car)
    {
		camShakeAllowed = true;
    }

	//getter and setter

	public Vector3 getCenter()
    {
		return center;
    }


	public void setCRisRunning(bool _myBool)
    {
		CRisRunning = _myBool;
    }

	public void setLastSmoothPos(Vector3 _CRResult)
    {
		lastSmoothPos = _CRResult;
    }











	//Trash Dump for obsolete Ideas and inspiration
	/*
	Vector3 smoothFavorChange(GameObject first) //gets a smooth movement between old and new firstPlaced when overtaking
	{

		//Vector3 startpoint = previous.transform.position;
		Vector3 startpoint = lastSmoothPos;

		Vector3 smoothPos = Vector3.MoveTowards(startpoint, first.transform.position, 0.1f); //That works!
																							 //Vector3 smoothPos = Vector3.SmoothDamp(startpoint, first.transform.position, ref velocity, 0.5f); //to slow and/or causes jitter

		lastSmoothPos = smoothPos;
		//Debug.Log("I´m smoothing movement between " + previous.name + " and " + first.name);
		return smoothPos;
		//return previous.transform.position;

	}

	Vector3 smoothFavorChangeOld(GameObject first) //gets a smooth movement between old and new firstPlaced when overtaking
	{
		if (placementManager.getPreviousFirstPlaced() != null) // && placementManager.getPreviousFirstPlaced() != first)
		{
			GameObject previous = placementManager.getPreviousFirstPlaced();
			//Vector3 startpoint = previous.transform.position;
			Vector3 startpoint = lastSmoothPos;


			Vector3 smoothPos = Vector3.MoveTowards(startpoint, first.transform.position, 20f * Time.deltaTime); //That works!
																												 //Vector3 smoothPos = Vector3.SmoothDamp(startpoint, first.transform.position, ref velocity, 0.5f); //to slow and/or causes jitter

			lastSmoothPos = smoothPos;
			Debug.Log("I´m smoothing movement between " + previous.name + " and " + first.name);
			return smoothPos;
			//return previous.transform.position;


		}
		else
		{
			return first.transform.position;
			//return lastSmoothPos;
		}

	}

	IEnumerator LerpPositionChangeBackup(Vector3 targetPosition, float duration) //Jumps when stopped from the outside. Works otherwise
	{
		CRisRunning = true;
		float time = 0;
		Vector3 startPosition = placementManager.getPreviousFirstPlaced().transform.position;
		while (time < duration)
		{
			if (placementManager.isOvertaken)
				Debug.Log("Overtake happens in the exact same frame. That sucks");
			targetPosition = placementManager.getFirstPlaced().transform.position;
			CRResult = Vector3.Lerp(startPosition, targetPosition, time / duration);
			time += Time.deltaTime;
			lastSmoothPos = CRResult;
			yield return null;
		}
		targetPosition = placementManager.getFirstPlaced().transform.position;
		lastSmoothPos = targetPosition;
		CRisRunning = false;
		yield break;
	}

	Vector3 factorInFirstPlaced(Bounds b) //watches for Values in Placement System and pushes CamCenter to firstPlaced
	{
		GameObject firstPlaced = placementManager.getFirstPlaced();
		//Debug.Log("Camera now favors " + firstPlaced.name);
		if (firstPlaced != null)
		{
			//Vector3 Lerp(Vector3 a, Vector3 b, float t);
			//Vector3 betweenFirsts = smoothFavorChange(firstPlaced);
			//Vector3 lipoCenter = Vector3.Lerp(b.center, betweenFirsts, favorFirstPlaced);
			Vector3 lipoCenter = Vector3.Lerp(b.center, firstPlaced.transform.position, favorFirstPlaced);
			return lipoCenter;
		}

		else
		{
			return b.center;
		}
	}

	//if I designed it well, this CoRoutine should become it's own class (less public variables) ~ Stickan
	IEnumerator LerpPositionChange(Vector3 startingPos, float duration, bool secondCall = false)
	{
		CRisRunning = true;
		float time = 0;
		Vector3 startPosition = startingPos;
		while (time < duration)
		{
			if (placementManager.isOvertaken)
			{
				waitForNextCall(secondCall);
				if (secondCall == true)
				{
					Debug.Log("Overtake happens in the exact same frame. That sucks");
					//StartCoroutine(LerpPositionChange(CRResult, 3));
					yield break;
				}
			}
			targetPosition = placementManager.getFirstPlaced().transform.position;
			CRResult = Vector3.Lerp(startPosition, targetPosition, time / duration);
			time += Time.deltaTime;
			lastSmoothPos = CRResult;
			yield return null;
		}
		targetPosition = placementManager.getFirstPlaced().transform.position;
		lastSmoothPos = targetPosition;
		CRisRunning = false;
		yield break;
	}

	void waitForNextCall(bool flag)
	{
		flag = true;
	}

	void oldMove()
	{
		Vector3 centerPoint = calcCenterPoint();

		Vector3 newPosition = centerPoint + offset;

		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
	}
*/

}
