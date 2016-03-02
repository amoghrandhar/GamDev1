using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public int playerNumber;


	public float speed;
	public float asteroidBoost;
	public bool clockwise;
	public KeyCode button;

	private Rigidbody rb;
	private GameObject asteroidObject;
	public GameObject arrow;

	public AudioSource audio;
	private GameController gc;

	private int asteroidCount;
	private float time;
	private bool released;
	private bool gameStarted;
	private float asteroidSpeed;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
		rb = this.GetComponent<Rigidbody> ();
		asteroidObject = GameObject.FindGameObjectWithTag ("Asteroid");
		released = true;
		asteroidCount = 0;
		arrow.GetComponent<Renderer> ().enabled = false;
		gameStarted = false;
		gc = (GameController) GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	// Update is called once per frame
	void Update () {

		// Check if any ships have launched
		if (!gc.isStarted ()) {

			// Start the game if ship launches
			if (Input.GetKey (button)) {
				audio.Play ();
				gc.startGame ();
				arrow.GetComponent<Renderer> ().enabled = false;
			}
		
		// Game has started
		} else {

			// Check if spaceship is orbiting
			if (!released) {

				// Check if player has launched spaceship
				if (Input.GetKey (button)) {

					released = true;
					audio.Play ();

					// Disable helper arrow when flying
					arrow.GetComponent<Renderer> ().enabled = false;

					// Match velocity of that when rotating
					float radius = Vector3.Distance (rb.transform.position, asteroidObject.transform.position);
					float angular = (asteroidBoost) * Mathf.Deg2Rad;

					// Calculate normal velocity from product of asteroid radius and angular velocity
					speed = radius * angular;

				}

				// Orient ship in direction of rotation
				rb.transform.LookAt (asteroidObject.transform);

				if (clockwise)
					rb.transform.RotateAround (rb.position, Vector3.up, 270.0f);
				else 
					rb.transform.RotateAround (rb.position, Vector3.up, 90.0f);

				// Move with asteroid
				transform.position += Vector3.back * Time.deltaTime;

				// Rotate around asteroid
				if (clockwise == true)
					rb.transform.RotateAround (asteroidObject.transform.position, Vector3.up, asteroidBoost * Time.deltaTime);
				else
					rb.transform.RotateAround (asteroidObject.transform.position, Vector3.up, -asteroidBoost * Time.deltaTime);

			} else { 
				rb.transform.Translate (Vector3.forward * Time.deltaTime * speed);
			}

		}

	}

	void OnTriggerEnter (Collider other) {

		// Check if collision object is boundary
		if (other.tag == "Boundary")
			return;

		// Check if collision object is an asteroid
		if (released == true && (other.tag == "Asteroid" || other.tag == "Asteroid1")) {

			// Activate helper arrow
			arrow.GetComponent<Renderer>().enabled = true;

			// Get references to asteroid gameobject and controller
			asteroidObject = other.attachedRigidbody.gameObject;
			AsteroidController astMover = asteroidObject.GetComponent<AsteroidController> ();

			// Find speed of current asteroid
			asteroidSpeed = astMover.speed;

			// Modify ship speed if it collides with a special asteroid
		    if (astMover.asteroidType == AsteroidController.AsteroidType.FAST)
				if (asteroidBoost < 320) asteroidBoost += 50 ;
		    else if (astMover.asteroidType == AsteroidController.AsteroidType.SLOW)
				if (asteroidBoost > 80) asteroidBoost -= 50;

			// Start moving earth if ship collides with it
			if (other.tag == "Asteroid1")
				astMover.speed = 0.8f;

			clockwise = DetermineRotationDirection (other.attachedRigidbody.gameObject);

			released = false;
			asteroidCount++;

		}

	}
		
	// Determines clockwise/anticlockwise spaceship rotation
	bool DetermineRotationDirection (GameObject go) {

		// Vector between the spaceship and asteroid
		Vector3 dir = (go.transform.position - rb.transform.position).normalized;

		// Dot product between direction vector and right side of ship
		float direction = Vector3.Dot (dir, transform.right);

		// Return true if asteroid is to the left of the ship
		return direction > 0;

	}

	public bool isReleased() {
		return released;
	}

	public int getAsteroidCount() {
		return asteroidCount;
	}

}