using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Timer Coroutines taken from: https://answers.unity.com/questions/1040630/do-something-for-10-sec.html
 */

public class TopDownCarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.95f;  //Set between 0 and 1. Lower value makes less drift,
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float steerRejectFactor = 8; //Sets amount of steering deficiency at low velocities
    public float maxSpeed = 20;
    public float startBoost = 20f;
    public int maxGear = 3;

    //local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 27;
    float currentVelocity = 0;

    Vector2 carPullVector;

    //flags for coroutines
    bool slipperyCRRunning = false;
    bool boostCRRunning = false;
    bool autoRotateCRRunning = false;

	private MapManager mapManager;

    //Components
    Rigidbody2D carRigidbody2D;

    //Awake is called when the script instance is being loaded.
    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        carPullVector = new Vector2(0.0f, 0.0f);
        carRigidbody2D.velocity = new Vector2(0, startBoost);
		mapManager = FindObjectOfType<MapManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(makeSlipperyForSeconds(0.99f, 10));
        }
    }

    //Frame-Rate independent for physics calculation
    void FixedUpdate()
    {
        ApplyEngineForce();

        KillOrthogonalVelocity();

        ApplySteering();

        PullCarBack(); //use if the camera should be static
    }

    void ApplyEngineForce()
    {
        //Calculates how much "forward" we are going in terms of the direction of our velocity
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Limit so we cannot go faster than the max speed in the "forward" direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        //Limit so we cannot go faster than 50% of max speed in the "reverse" direction
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
            return;

        //Limit so we cannot go faster in any direction while accelerating
        currentVelocity = carRigidbody2D.velocity.sqrMagnitude;
        if (currentVelocity > maxSpeed * maxSpeed && accelerationInput > 0)
            return;

        //Apply drag if there is no accelerationInput so the car stops when the player lets go of the accelerator
        if (accelerationInput == 0)
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        else carRigidbody2D.drag = 0;

		//float groundTileResistance = mapManager.GetTileResistance(transform.position);	// gets the Value for the resistance of the current Ground Tile. Applies to the Engine Force next line

        //Creates a force for the Engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor; // * (1 / groundTileResistance);

        //Applies force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //Limits the cars ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / steerRejectFactor);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        //Updates the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        //Apply steering by rotating the car object
        carRigidbody2D.MoveRotation(rotationAngle);
        //Debug.Log("Rotatoin Angle: " + rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //reduces the forces to the sides and minimizes drifting
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;

    }

    void PullCarBack()
    {
        carRigidbody2D.AddForce(carPullVector);
        //Debug.Log("carPullVector y-direction is right now" + carPullVector.y);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public void SetPullVector(Vector2 pullVector)
    {
        carPullVector = pullVector;
        //Debug.Log("The Car is currently pulled to direction:" + pullVector); //Prints the Value of the Pullback Vector to Console
    }

    //only really necessary for sound atm
    public int currentGear()
    {
        int currentGear;
        float normalizedVelocity = getRelativeCarVelocity();
        currentGear = Mathf.CeilToInt(normalizedVelocity * maxGear);
        return currentGear;
    }


    //application of external forces
    //acessed by CarCollisionManager through Obstacle scripts

    public void adjustRotationAngle(float difference)
    {
        //rotationAngle += difference*Time.deltaTime;
        rotationAngle += difference;
        //Debug.Log("Rotatoin Angle adjusted");
    }

    public void autoRotateCar(float spinAmount, float spinVelocity)
    {
        if (autoRotateCRRunning == false)
        {
           
            StartCoroutine(spinCarFromOutside(spinAmount, spinVelocity));
        }
    }

    public void addAdditionalVelocity(Vector2 forwardForce, float _duration)
    {
        if (boostCRRunning)
        {
            StopCoroutine(additionalVelocityForSeconds(forwardForce, _duration));
            StartCoroutine(additionalVelocityForSeconds(forwardForce, _duration));
        }
        else
            StartCoroutine(additionalVelocityForSeconds(forwardForce, _duration));

    }

    public void changeDriftFactor(float tempDriftFactor, float duration)
    {
        if (slipperyCRRunning == false)
        {
            StartCoroutine(makeSlipperyForSeconds(tempDriftFactor, duration));
        }
        else
            Debug.Log("Cars already slipping");
    }


    private IEnumerator additionalVelocityForSeconds(Vector2 _forwardForce, float timeSeconds)
    {
        boostCRRunning = true;
        for (int i = 0; i < 10000; i++)
        {
            while (timeSeconds >= 0)
            {
                carRigidbody2D.AddForce(transform.up * _forwardForce, ForceMode2D.Force);
               // Debug.Log(i++);
                timeSeconds -= Time.smoothDeltaTime;
                yield return null;
            }
        }
        boostCRRunning = false;
    }

    private IEnumerator spinCarFromOutside(float spins, float spinVelocity)
    {
        autoRotateCRRunning = true;

        float maxSpinAngle = spins * 360;
        float alreadySpun = 0;

        while (alreadySpun < maxSpinAngle && alreadySpun > -maxSpinAngle)
        {
            //float slowSpin = oilConstant / 2;
            adjustRotationAngle(spinVelocity);
            alreadySpun += spinVelocity;
            yield return null;
        }
        autoRotateCRRunning = false;
        yield break;
    }

    private IEnumerator makeSlipperyForSeconds(float newDriftFactor, float timeSeconds)
    {
        slipperyCRRunning = true;
        float oldDriftFactor = driftFactor;
        driftFactor = newDriftFactor;

        for (int i = 0; i < 10000; i++)
        {
            while (timeSeconds >= 0)
            {                
               // Debug.Log(i++);
                timeSeconds -= Time.smoothDeltaTime;
                yield return null;
            }
        }
        driftFactor = oldDriftFactor;
        slipperyCRRunning = false;
    }

    private IEnumerator countdownInSeconds(float timeSeconds)
    {
        for (int i = 0; i < 10000; i++)
        {
            while (timeSeconds >= 0)
            {
                //Debug.Log(i++);
                timeSeconds -= Time.smoothDeltaTime;
                yield return null;
            }
        }
    }

    //getter and setter
    public bool getSlipperyStatus()
    {
        return slipperyCRRunning;
    }

    public float getRelativeCarVelocity()
    {
        return currentVelocity / (maxSpeed*maxSpeed);
    }

    public float getAbsoluteCarVelocity()
    {
        return currentVelocity;
    }

}
