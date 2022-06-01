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
	Camera _mainCamera;

	//[SerializeField]
	//GameObject car;

	public List<Transform> targets;
	public PartTakingCarsListSO allCars;
	public Vector3 offset;
	public float smoothTime = .5f;
	private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
		_mainCamera = GetComponent<Camera>();

		makeTargetsList();
    }

    // Update is called once per frame
    void Update()
    {
		//version von Marv
        //transform.position = new Vector3(car.transform.position.x, car.transform.position.y, 0);
    }

	void LateUpdate()
	{
		if (targets.Count == 0)
			return;

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
		return bounds.center;
	}

	void makeTargetsList()
	{
		foreach(GameObject car in allCars.carsList)
		{
			targets.Add(car.transform);
		}
	}
}
