using UnityEngine;
using System.Collections;

public class CommetController : MonoBehaviour {

	public int health = 5;


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
		if (health <= 0) {
			Destroy(gameObject);
		}
	}
}
