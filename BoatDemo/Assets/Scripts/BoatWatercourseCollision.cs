using UnityEngine;
using System.Collections;

public class BoatWatercourseCollision : MonoBehaviour {
	
	private Vector3 originalPosition;
	private Quaternion originalRotation;
	private GameObject Boat;
	private ShipControls controls;

	// Use this for initialization
	void Start () {
		Boat = GameObject.Find("Water_Powerboat");
		if (Boat) {
			originalPosition = Boat.transform.position;
			originalRotation = Boat.transform.rotation;
			controls = Boat.GetComponent<ShipControls>();
			if (!controls) Debug.Log("BoatWatercourseCollisions: No ShipControls Reference. This is a bug.");
		}
		else {
			Debug.Log("BoatWatercourseCollisions: No Boat Reference. This is a bug.");
		}
	}
	
	// Update is called once per frame
	void Update () {}
	
	// For trigger collision detection.
	void OnTriggerEnter(Collider other) {
		if (Boat && controls) {
			if(other.gameObject.tag == "Water_Course") {
				controls.collisionDetected = true;
				Boat.transform.position = originalPosition;
				Boat.transform.rotation = originalRotation;
				if (Boat.rigidbody != null) {
					Boat.rigidbody.velocity = Vector3.zero;
					Boat.rigidbody.angularVelocity = Vector3.zero;
					Boat.rigidbody.angularDrag = 0f;
					Boat.rigidbody.drag = 0f;
				}
			}
		}
	}
}
