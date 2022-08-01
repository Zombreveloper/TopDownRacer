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

	//helping variables
	Vector3 lastSmoothPos;
	Vector3 center;
	Vector3 CRResult; //result of the Coroutine
	bool CRisRunning = false;

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
		//Vector3 betweenPos = firstPlacedPos;
		
		if (placementManager.isOvertaken)
        {
			StartCoroutine(LerpPositionChange(firstPlacedPos, 1)); //must get somewhere else. calls way to much subroutines at same time
		}
		

		Vector3 betweenPos = BetweenPos(firstPlacedPos); //function to use to update values even if coroutine not running
		//Vector3 betweenPos = CRResult;


		Vector3 centerPoint = getCenterPoint();
		center = Vector3.SmoothDamp(center, centerPoint, ref velocity, smoothTime);

		Vector3 lipoCenter = Vector3.Lerp(center, betweenPos, favorFirstPlaced);

		//transform.position = lipoCenter;
		//transform.position = center; //(for testing) gives only the center between all cars 
		transform.position = betweenPos; //if you only want the camPos between first and second place (for testing)
	}

	Vector3 BetweenPos(Vector3 _firstPlacedPos)
	{
		if (CRisRunning == true) //in andere Funktion auslagern mit Rückgabewert und dort betweenPos initiieren und ausgeben. Einschließlich Deklaration da oben!
		{
			//Vector3 betweenPos = CRResult;
			return CRResult;
		}
		else
		{
			//Vector3 betweenPos = firstPlacedPos;
			Debug.Log("Coroutine is not running or bool is wrong");
			return _firstPlacedPos;
		}
	}

	//TargetPosition doesn't update while CR is running. use global value instead!
	IEnumerator LerpPositionChange(Vector3 targetPosition, float duration)
	{
		CRisRunning = true;
		float time = 0;
		Vector3 startPosition = lastSmoothPos;
		while (time < duration)
		{
			CRResult = Vector3.Lerp(startPosition, targetPosition, time / duration);
			time += Time.deltaTime;
			lastSmoothPos = CRResult;
			yield return null;
		}
		lastSmoothPos = targetPosition;
		CRisRunning = false;
		yield break;
	}

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

	//watches for Values in Placement System and pushes CamCenter to firstPlaced
	Vector3 factorInFirstPlaced(Bounds b)
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
}
