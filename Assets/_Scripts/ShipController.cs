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

		if (!gc.isStarted ()) {

			if (Input.GetKey (button)) {
				audio.Play ();
				gc.startGame ();
				arrow.GetComponent<Renderer> ().enabled = false;
			}

		} else {
			if (!released) {
				if (Input.GetKey (button)) {

					released = true;
					audio.Play ();
					arrow.GetComponent<Renderer> ().enabled = false;

					float radius = Vector3.Distance (rb.transform.position, asteroidObject.transform.position);
					float angular = (asteroidBoost) * Mathf.Deg2Rad;

					speed = radius * angular;


					//rb.AddForce (new Vector3 (0, 0, -500));
					//rb.AddForce (rb.transform.Translate(-Vector3.forward * Time.deltaTime*100));
				}

				// Orient ship in direction of rotation
				rb.transform.LookAt (asteroidObject.transform);
				if (clockwise) {
					rb.transform.RotateAround (rb.position, Vector3.up, 270.0f);
				} else {
					rb.transform.RotateAround (rb.position, Vector3.up, 90.0f);
				}



				// Move with asteroid
				transform.position += -asteroidSpeed * Vector3.forward * Time.deltaTime;


				// Rotate around asteroid
				if (clockwise == true) {
					rb.transform.RotateAround (asteroidObject.transform.position, Vector3.up, asteroidBoost * Time.deltaTime);
				} else {
					rb.transform.RotateAround (asteroidObject.transform.position, Vector3.up, -asteroidBoost * Time.deltaTime);
				}

			} else { 
				rb.transform.Translate (Vector3.forward * Time.deltaTime * speed);
			}
		}
	}


	/*
	void Awake(){
		ship = transform;
		rb = this.GetComponent<Rigidbody> ();
		rb.AddForce (transform.forward * 100);
		rb.AddForce (transform.up * 100);

	}

	// Use this for initialization
	void Start () {
		

		GameObject asteroidObject = GameObject.FindGameObjectWithTag ("Asteroid");
		asteroidTransform = asteroidObject.transform; 
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 line = asteroidTransform.position - ship.position;
		line.Normalize ();

		float distance = Vector3.Distance (asteroidTransform.position, ship.position);
		rb.AddForce (line * 10 / distance);
	}
	*/

	void OnTriggerEnter (Collider other) {

		if (other.tag == "Boundary")
			return;

		if (released == true && (other.tag == "Asteroid" || other.tag == "Asteroid1")) {

			arrow.GetComponent<Renderer>().enabled = true;
			
			asteroidObject = other.attachedRigidbody.gameObject;

			AsteroidController astMover = asteroidObject.GetComponent<AsteroidController> ();
			asteroidSpeed = astMover.speed;

		    if (astMover.asteroidType == AsteroidController.AsteroidType.FAST) {
				if (asteroidBoost < 300) asteroidBoost += 50 ;
		    } else if (astMover.asteroidType == AsteroidController.AsteroidType.SLOW) {
				if (asteroidBoost > 80) asteroidBoost -= 50;
		    }

			clockwise = DetermineRotationDirection (other.attachedRigidbody.gameObject);

			if (other.tag == "Asteroid1")
				astMover.speed = 0.8f;

			released = false;
			asteroidCount++;

			/*
			RaycastHit hit;
			var ray = new Ray(transform.position, Vector3.down);
			if (other.Raycast(ray, out hit, 1000)) {
				transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
			}
			*/
		}

	}

	bool DetermineRotationDirection (GameObject go) {

		Vector3 dir = (go.transform.position - rb.transform.position).normalized;

		float direction = Vector3.Dot (dir, transform.right);

		// Asteroid is to the left of the ship
		return direction > 0;

	}

	public bool isReleased() {
		return released;
	}

	public int getAsteroidCount() {
		return asteroidCount;
	}

}