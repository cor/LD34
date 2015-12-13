using UnityEngine;
using System.Collections;

public class SpaceshipController : MonoBehaviour {

	public float speed = 5;

	public Transform leftThrust1;
	public Transform leftThrust2;

	public Transform rightThrust1;
	public Transform rightThrust2;

	public bool leftThrustActivated;
	public bool rightThrustActivated;

	public GameObject bullet;

	public float timeBetweenShots = 0.1f;
	float timeSinceLastShot;

	Rigidbody2D rb;
	// Use this for initialization
	void Start () {

		// initialize properties
		rb = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void Update () {
		bool leftDown = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
		bool rightDown = Input.GetKey(KeyCode.D) ||Input.GetKey(KeyCode.RightArrow);

		leftThrustActivated = leftDown;
		rightThrustActivated = rightDown;

		Debug.Log("Left: " + leftDown);
		Debug.Log("Right: " + rightDown);


		ApplyThrusts();
	}

	void ApplyThrusts() {

		Vector2 rightThrustVector = rightThrust2.position - rightThrust1.position;
		Vector2 leftThrustVector = leftThrust2.position - leftThrust1.position;

		bool shotsFired = false;

		if (leftThrustActivated) {
			rb.AddForceAtPosition(leftThrustVector * speed * -1f, leftThrust1.position);

			if (timeSinceLastShot + timeBetweenShots < Time.time) {
				GameObject bulletClone = (GameObject) (Instantiate(bullet, leftThrust1.position, transform.rotation));
				bulletClone.GetComponent<Rigidbody2D>().velocity = leftThrustVector * 10f;
				shotsFired = true;
			}
		}

		if (rightThrustActivated) {
			rb.AddForceAtPosition(rightThrustVector * speed * -1f, rightThrust1.position);

			if (timeSinceLastShot + timeBetweenShots < Time.time) {
				GameObject bulletClone = (GameObject) (Instantiate(bullet, rightThrust1.position, transform.rotation));
				bulletClone.GetComponent<Rigidbody2D>().velocity = rightThrustVector * 10f;
				shotsFired = true;
			}
		}

		if (shotsFired) {
			timeSinceLastShot = Time.time;
		}

		// Adjust drag for finer moevement
		if ( leftThrustActivated && rightThrustActivated ) {
			rb.angularDrag = 2f;

		} else if ( leftThrustActivated || rightThrustActivated ) {
			rb.angularDrag = 1.6f;
		
		} else if ( !(leftThrustActivated || rightThrustActivated )) {
			rb.angularDrag = 3f;
		} else {
			rb.angularDrag = 1f;
		}
	}

}
