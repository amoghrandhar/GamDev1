﻿using UnityEngine;
using System.Collections;

public class OrbitingScript : MonoBehaviour {
	public int speed;

	private Rigidbody rb;
	private GameObject asteroidObject;
	private bool released;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		asteroidObject = GameObject.FindGameObjectWithTag ("Asteroid");
		released = false;
	}

	// Update is called once per frame
	void Update () {
		if (!released) {
			if (Input.GetButton ("Fire1")) {
				released = true;

				//rb.AddForce (new Vector3 (0, 0, -500));
				//rb.AddForce (rb.transform.Translate(-Vector3.forward * Time.deltaTime*100));
			}

			rb.transform.RotateAround (asteroidObject.transform.position, Vector3.up, 100 * Time.deltaTime);

		} else {
			rb.transform.Translate (-Vector3.forward * Time.deltaTime * speed );

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

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Asteroid")
		{
			;
		}

	}*/
}