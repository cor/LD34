using UnityEngine;
using System.Collections;

public class CommetController : MonoBehaviour {

	public int health = 5;

	private bool died = false;
	Vector2 directionBeforeDeath;
	float timeOfDeath;


	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Bullet") {
			--health;
			Destroy(collision.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!died) {
			if (health <= 0) {
				Die();
			}
		} else {
			// dead
			transform.position = new Vector3(transform.position.x + (directionBeforeDeath.x * 0.1f * ((Time.time - timeOfDeath) * 5)), 
				transform.position.y + (directionBeforeDeath.y * 0.1f * ((Time.time - timeOfDeath) * 5)),
				transform.position.z - 0.05f);

			transform.localScale *= 1.03f;
		}

	}

	void Die() {
		died = true;
		timeOfDeath = Time.time;

		transform.position = new Vector3(transform.position.x, transform.position.y, -3f);


		directionBeforeDeath = GetComponent<Rigidbody2D>().velocity;


		if (GetComponent<Rigidbody2D>() != null) { Destroy(GetComponent<Rigidbody2D>()); }
		if (GetComponent<CircleCollider2D>() != null) { Destroy(GetComponent<CircleCollider2D>()); }

		Destroy (gameObject, 1.5f);
	}
}
