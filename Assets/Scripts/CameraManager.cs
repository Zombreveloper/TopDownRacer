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
	//[SerializeField]
	//GameObject car;

	public List<Transform> targets;


	public Vector3 offset;

	public float smoothTime = .5f;
	private Vector3 velocity;
	public float zoomLimiter = 30f;
	public float minZoom = 30f;
	public float maxZoom = 15f;

	public float favorFirstPlaced = 0.6f;

	//global variables for Lipo in Coroutine (short: CR)
	Vector3 lastSmoothPos;
	Vector3 CRResult; //result of the Coroutine
	Vector3 targetPosition; //target, the Lipo goes to
	bool CRisRunning = false;

	//global variables
	Vector3 center;

	// Start is called before the first frame update
	void Start()
	{
		mainCamera = GetComponent<Camera>();
		placementManager = GameObject.Find("/PlacementManager").GetComponent<PlacementManager>();

		makeTargetsList();

		lastSmoothPos = activeCars.carsList[0].transform.position;
		//placementManager.getFirstPlaced().transform.position;
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

		newMove();
		zoom();
	}

	void move()
	{
		Vector3 centerPoint = getCenterPoint();

		Vector3 newPosition = centerPoint + offset;

		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
	}
	void newMove()
	{
		//Vector3 betweenPos = smoothFavorChange(placementManager.getFirstPlaced());
		Vector3 firstPlacedPos = placementManager.getFirstPlaced().transform.position;
		

		if (placementManager.isOvertaken)
		{
			Vector3 prevFirstPos = placementManager.getPreviousFirstPlaced().transform.position;

			StopAllCoroutines(); //stops a coroutine from ever setting CRisRunning to false while another CR is running
			if (CRisRunning == false) //if this value is true the CR must have been stopped prematurely. To keep smooth movement the start point needs to be different
			{
				//StartCoroutine(LerpPositionChange(prevFirstPos, 3)); //Calls old Lerp-function
				StartCoroutine(LerpInCenterSpace(placementManager.getPreviousFirstPlaced(), placementManager.getFirstPlaced(), 1));
			}
			else
            {
				Debug.Log("does this ever get called?");
				//StartCoroutine(LerpPositionChange(lastSmoothPos, 3)); //Calls old Lerp-function
				StartCoroutine(LerpInCenterSpace(lastSmoothPos, placementManager.getFirstPlaced(), 1));
			}
				
		}


		Vector3 betweenPos = BetweenPos(firstPlacedPos); //function to use to update values even if coroutine not running


		Vector3 centerPoint = getCenterPoint();
		center = Vector3.SmoothDamp(center, centerPoint, ref velocity, smoothTime);

		Vector3 lipoCenter = Vector3.Lerp(center, betweenPos, favorFirstPlaced);

		transform.position = lipoCenter;
		//transform.position = center; //(for testing) gives only the center between all cars 
		//transform.position = betweenPos; //if you only want the camPos between first and second place (for testing)
	}

	Vector3 BetweenPos(Vector3 _firstPlacedPos)
	{
		if (CRisRunning) //in andere Funktion auslagern mit Rückgabewert und dort betweenPos initiieren und ausgeben. Einschließlich Deklaration da oben!
		{
			//Vector3 betweenPos = CRResult;
			//Debug.Log("Coroutine is running");
			return lastSmoothPos;
		}
		else
		{
			//Vector3 betweenPos = firstPlacedPos;
			//Debug.Log("Coroutine is not running");
			//Debug.Log("CRisRunning is now false!");
			return _firstPlacedPos;
		}
	}



	void waitForNextCall(bool flag)
    {
		flag = true;		
    }

	//if I designed this well, this CoRoutine should become it's own class (less public variables) ~ Stickan
	IEnumerator LerpInCenterSpace(GameObject secondCar, GameObject firstCar, float duration)
    {
		CRisRunning = true;
		float time = 0;
		Vector3 startPoint = secondCar.transform.position;
		Vector3 startPosition = WorldToCenterSpace(startPoint); //startPosition doesn't get updated but moves with the camera
		while (time < duration)
		{
			Vector3 targetCarPosition = firstCar.transform.position;
			Vector3 targetPosition = WorldToCenterSpace(targetCarPosition); //targetPosition updates every frame

			float t = time / duration; //function that smoothes the transition curve somewhat. If you want linear movement, replace t with time/duration in Vector3.Lerp down below
			t = t * t * (3f - 2f * t);

			CRResult = Vector3.Lerp(startPosition, targetPosition, t); //result is in CenterSpace
			CRResult = CenterToWorldSpace(CRResult);
			time += Time.deltaTime;
			lastSmoothPos = CRResult;
			yield return null;
		}
		//transform.position = targetPosition;
		//lastSmoothPos = firstCar.transform.position; //Without Lipo We don't need a conversion to CenterSpace
		Debug.Log("CRisRunning is set false here!");
		CRisRunning = false;
	}

	IEnumerator LerpInCenterSpace(Vector3 startPoint, GameObject firstCar, float duration)
	{
		CRisRunning = true;
		float time = 0;
		//Vector3 startPoint = secondCar.transform.position;
		Vector3 startPosition = WorldToCenterSpace(startPoint); //startPosition doesn't get updated but moves with the camera
		while (time < duration)
		{
			Vector3 targetCarPosition = firstCar.transform.position;
			Vector3 targetPosition = WorldToCenterSpace(targetCarPosition); //targetPosition updates every frame

			float t = time / duration;  //function that smoothes the transition curve somewhat. If you want linear movement, replace t with time/duration in Vector3.Lerp down below
			t = t * t * (3f - 2f * t);

			CRResult = Vector3.Lerp(startPosition, targetPosition, t); //result is in CenterSpace
			CRResult = CenterToWorldSpace(CRResult);
			time += Time.deltaTime;
			lastSmoothPos = CRResult;
			yield return null;
		}
		//transform.position = targetPosition;
		//lastSmoothPos = firstCar.transform.position; //Without Lipo We don't need a conversion to CenterSpace
		Debug.Log("CRisRunning is set false here!");
		CRisRunning = false;
	}

	Vector3 WorldToCenterSpace(Vector3 point) //converts ObjectPositions from WorldSpace to a Space that is relative to the CenterPoint between all cars
    {
		Vector3 newPoint = point - center;
		return newPoint;
    }

	Vector3 CenterToWorldSpace(Vector3 point) //converts ObjectPositions from CenterSpace Back to normal WorldSpace
	{
		Vector3 newPoint = point + center;
		return newPoint;
	}

	//from here on everything that is not Coroutine Lipo related

	Vector3 getCenterPoint()
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

		//return factorInFirstPlaced(bounds);
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

	void zoom()
	{
		//Debug.Log(getGreatestDistance());
		float newZoom = Mathf.Lerp(maxZoom, minZoom, getGreatestDistance() / zoomLimiter);
		mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, newZoom, Time.deltaTime);
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












	//Trash Dump for obsolete Ideas and inspiration
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
		/*TODO
		 * when cars are overtaking while camera panning not complete, cam will jump
		 * instead of previous car position use cam position for interpolation!
		 */
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

}
