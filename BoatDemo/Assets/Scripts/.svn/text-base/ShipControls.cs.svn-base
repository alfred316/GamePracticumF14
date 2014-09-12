// This script was used to provide realistic boat movement.

// Removed two depreciated functions.  Used Deg2Rad and back again because I don't know what I'm doing.  Anyone is welcome to clean this up properly.  -WarpZone
 
using UnityEngine;
using System.Collections;
 
// Put this on a rigidbody object and instantly
// have 2D spaceship controls like OverWhelmed Arena
// that you can tweak to your heart's content.
 
// Feel free to make any of these fields public so you can tweak them during run-time.
[RequireComponent (typeof (Rigidbody))]
public class ShipControls : MonoBehaviour
{
    private float hoverHeight = 63.5f;
    private float hoverHeightStrictness = 1;
    private float forwardThrust = -5000;
    private float turnSpeed = 100;
 
    private Vector3 forwardDirection = new Vector3(1, 0, 0);
 
    private float mass = 1;
 
    // positional drag
    private float sqrdSpeedThresholdForDrag = 25;
    private float superDrag = 2;
    private float fastDrag = 0.5f;
    private float slowDrag = 0.01f;
 
    // angular drag
    private float sqrdAngularSpeedThresholdForDrag = 5;
    private float superADrag = 32;
    private float fastADrag = 16;
    private float slowADrag = 0.1f;
	
	// Outboard motor transform
	private Transform outboardMotor;
	
	// Outboard motor rotation
	private Vector3 curOrientation = Vector3.zero;
	private Vector3 startPos = new Vector3(0, 90, 90);
	private Vector3 lDirection = new Vector3(-30, 90, 90);
	private Vector3 rDirection = new Vector3(30, 90, 90);
	
	public bool collisionDetected = false;
 
    void Start()
    {
        rigidbody.mass = mass;
		outboardMotor = transform.Find("connectToBoat");
		curOrientation = startPos;
		outboardMotor.localEulerAngles = curOrientation;
    }
 
    void FixedUpdate()
    {
        if (Mathf.Abs(thrust) > 0.01F)
        {
            if (rigidbody.velocity.sqrMagnitude > sqrdSpeedThresholdForDrag)
                rigidbody.drag = fastDrag;
            else
                rigidbody.drag = slowDrag;
        }
        else
            rigidbody.drag = superDrag;
 
        if (Mathf.Abs(turn) > 0.01F)
        {
            if (rigidbody.angularVelocity.sqrMagnitude > sqrdAngularSpeedThresholdForDrag)
                rigidbody.angularDrag = fastADrag;
            else
                rigidbody.angularDrag = slowADrag;
        }
        else
            rigidbody.angularDrag = superADrag;
 
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, hoverHeight, transform.position.z), hoverHeightStrictness);
    }
 
    float thrust = 0F;
    float turn = 0F;
 
    public IEnumerator Thrust(float t, float seconds)
    {
        thrust = Mathf.Clamp(t, -1F, 1F); // thrust boat with force t
		yield return new WaitForSeconds(seconds); // thrust boat with force t for a few seconds
		thrust = Mathf.Clamp(0, -1F, 1F); // set thrust to zero, bringing boat to smooth hault
    }
 
    public IEnumerator Turn(float t, float seconds)
    {
        turn = Mathf.Clamp(t, -1F, 1F) * turnSpeed;
		yield return new WaitForSeconds(seconds);
		turn = Mathf.Clamp(0, -1F, 1F) * turnSpeed;
    }
	
	public IEnumerator MoveTheBoat(Queue movementQueue) {
		// convert queue to array in order to provide constant time random access
		System.Object[] movementArray = movementQueue.ToArray();
		
		for (int i = 0; i < movementArray.Length - 2; i += 3) {
			Debug.Log ("Called MoveTheBoat with values: " + movementArray[i] + ", " + movementArray[i+1] + ", " + movementArray[i+2] + ", at i = " + i);
			StartCoroutine(Thrust((float)movementArray[i], (float)movementArray[i+2]));
			StartCoroutine(Turn((float)movementArray[i+1], (float)movementArray[i+2]));
			
			// pause
			if (!collisionDetected)
				yield return new WaitForSeconds((float)movementArray[i+2]);
		}
		
		// clear the queue
		movementQueue.Clear();
		collisionDetected = false;
	}
	
    void Update ()
    {
		// Raycast to detect upcoming obstacles
		RaycastHit hit;
		Vector3 rayPos = transform.position; // position of ray
		Vector3 rayDir = transform.TransformDirection(Vector3.left); // direction of ray
		float rayMag = 150; // magnitude of ray vector
		Ray radarRay = new Ray(rayPos, rayDir);
		
		Debug.DrawRay(rayPos, rayDir * rayMag); // see what ray looks like in scene view
		
		if (Physics.Raycast(radarRay, out hit, rayMag)) // cast ray, returns true if collision occurs
		{
			if (hit.collider.tag == "Water_Course")
				Debug.Log("Raycast hit " + Time.deltaTime);
		}
		
		if (collisionDetected)
		{
			Debug.Log("Collision Detected.");
			return;
		}
		
		float theThrust = thrust;
		
        if (theThrust > 0F)
        {
			theThrust *= forwardThrust;
			
			// here we rotate the outboard motor depending on which direction the boat is turning
			if (turn < 0)
			{
				curOrientation = Vector3.Lerp(curOrientation, lDirection, Time.deltaTime);
				outboardMotor.localEulerAngles = curOrientation;
			}
			else if (turn > 0)
			{
				curOrientation = Vector3.Lerp(curOrientation, rDirection, Time.deltaTime);
				outboardMotor.localEulerAngles = curOrientation;
			}
        }
		
		// reset the outboard motor to normal position after the boat is done turning
		if (turn == 0 && curOrientation != startPos)
		{
			curOrientation = Vector3.Lerp(curOrientation, startPos, Time.deltaTime);
			outboardMotor.localEulerAngles = curOrientation;
		}
		
        rigidbody.AddRelativeTorque(Vector3.forward * turn * Time.deltaTime); // turn boat
        rigidbody.AddRelativeForce(forwardDirection * theThrust * Time.deltaTime); // move boat
    }
}