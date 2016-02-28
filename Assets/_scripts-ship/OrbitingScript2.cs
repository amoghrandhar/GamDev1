using UnityEngine;
using System.Collections;

public class OrbitingScript2 : MonoBehaviour {

	public int startSpeed;
	public int speed;
	public bool clockwise;  // True = 

	private Rigidbody rb;
	private GameObject asteroidObject;
	private bool released;


	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		asteroidObject = GameObject.FindGameObjectWithTag ("Asteroid");
		released = true;
	}

	// Update is called once per frame
	void Update () {

		if (!released) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				released = true;

				rb.transform.LookAt (asteroidObject.transform);

				if(clockwise == true)
					rb.transform.RotateAround(rb.position, Vector3.up, 270.0f);
				else
					rb.transform.RotateAround(rb.position, Vector3.up, 90.0f);

				//rb.AddForce (new Vector3 (0, 0, -500));
				//rb.AddForce (rb.transform.Translate(-Vector3.forward * Time.deltaTime*100));
			}

			if (clockwise == true)
				rb.transform.RotateAround (asteroidObject.transform.position, Vector3.up, 100 * Time.deltaTime);
			else
				rb.transform.RotateAround (asteroidObject.transform.position, Vector3.up, -100 * Time.deltaTime);

		}  else {

			rb.transform.Translate (Vector3.forward * Time.deltaTime * speed);
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

		asteroidObject = other.attachedRigidbody.gameObject;
		Debug.Log ("Coliided " + other.tag);

		if (released == true && other.tag == "Asteroid") {
			clockwise = DetermineRotationDirection (other.attachedRigidbody.gameObject);
			released = false;

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

}
