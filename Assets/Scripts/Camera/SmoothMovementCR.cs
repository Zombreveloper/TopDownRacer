using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Linear Interpolation inspired by: https://gamedevbeginner.com/the-right-way-to-lerp-in-unity-with-examples/
 * Takes values from the camera manager and interpolates them. Returns the results (position between car 1 and car 2)
 * and a bool if the Lerp is running
*/

public class SmoothMovementCR : MonoBehaviour
{
	//referenced classes
	private CameraManager camManager;

	//used variables
	//Vector3 lastSmoothPos;
	Vector3 CRResult; //result of the Coroutine
	Vector3 targetPosition; //target, the Lipo goes to

	// Start is called before the first frame update
	void Start()
    {
		camManager = GetComponent<CameraManager>();
    }


	public IEnumerator LerpInCenterSpace(GameObject secondCar, GameObject firstCar, float duration)
	{
		//CRisRunning = true;
		camManager.setCRisRunning(true);
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
			camManager.setLastSmoothPos(CRResult);
			yield return null;
		}
		Debug.Log("CRisRunning is set false here!");
		camManager.setCRisRunning(false);
	}

	public IEnumerator LerpInCenterSpace(Vector3 startPoint, GameObject firstCar, float duration)
	{
		//CRisRunning = true;
		camManager.setCRisRunning(true);
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
			camManager.setLastSmoothPos(CRResult);
			yield return null;
		}
		Debug.Log("CRisRunning is set false here!");
		camManager.setCRisRunning(false);
	}

	//conversion is important so that in WorldSpace the Lipo start point doesn`t leave the visible area
	Vector3 WorldToCenterSpace(Vector3 point) //converts ObjectPositions from WorldSpace to a Space that is relative to the CenterPoint between all cars
	{
		Vector3 center = camManager.getCenter();
		Vector3 newPoint = point - center;
		return newPoint;
	}

	Vector3 CenterToWorldSpace(Vector3 point) //converts ObjectPositions from CenterSpace Back to normal WorldSpace
	{
		Vector3 center = camManager.getCenter();
		Vector3 newPoint = point + center;
		return newPoint;
	}

}
