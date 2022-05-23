using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//private int ScreenWidth = 16;
//private int ScreenHeight = 9;

public class CameraManager : MonoBehaviour
{
	Camera _mainCamera;
	
	[SerializeField] 
	GameObject car;
	
    // Start is called before the first frame update
    void Start()
    {
		_mainCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(car.transform.position.x, car.transform.position.y, 0);
    }
}
