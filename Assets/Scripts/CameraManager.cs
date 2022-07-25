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
	Camera _mainCamera;
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

    // Start is called before the first frame update
    void Start()
    {
		_mainCamera = GetComponent<Camera>();
		placementManager = GameObject.Find("/PlacementManager").GetComponent<PlacementManager>();

		makeTargetsList();
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
	}

	void move()
	{
		Vector3 centerPoint = getCenterPoint();

		Vector3 newPosition = centerPoint + offset;

		transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
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
		//return bounds.center;

		return factorInFirstPlaced(bounds);
	}

	//watches for Values in Placement System and pushes CamCenter to firstPlaced
	Vector3 factorInFirstPlaced(Bounds b)
    {
		GameObject firstPlaced = placementManager.getFirstPlaced();
		//Debug.Log("Camera now favors " + firstPlaced.name);
		if (firstPlaced != null)
		{
			//Vector3 Lerp(Vector3 a, Vector3 b, float t);
			Vector3 betweenFirsts = smoothFavorChange(firstPlaced);
			Vector3 lipoCenter = Vector3.Lerp(b.center, betweenFirsts, favorFirstPlaced);
			return lipoCenter;
		}

		else
		{
			return b.center;
		}
	}

	Vector3 smoothFavorChange(GameObject first) //gets a smooth movement between old and new firstPlaced when overtaking
    {
		/*TODO
		 * when cars are overtaking while camera panning not complete, cam will jump
		 * instead of previous car position use cam position for interpolation!
		 */
		if (placementManager.getPreviousFirstPlaced() != null)
        {
			GameObject previous = placementManager.getPreviousFirstPlaced();
			Vector3 startpoint = previous.transform.position;
			if (placementManager.isOvertaken)
            {
				//startpoint = this.gameObject.transform.position;
				startpoint = previous.transform.position;
			}

			Vector3 smoothPos = Vector3.SmoothDamp(startpoint, first.transform.position, ref velocity, 10f);
			//Debug.Log("I´m smoothing movement");
			return smoothPos;
			//Vector3 smoothPos = Vector3.SmoothDamp(previous.transform.position, first.transform.position, ref velocity, 1f);

		}
		else
        {
			return first.transform.position;
        }
		
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
		_mainCamera.orthographicSize = Mathf.Lerp(_mainCamera.orthographicSize, newZoom, Time.deltaTime);
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
