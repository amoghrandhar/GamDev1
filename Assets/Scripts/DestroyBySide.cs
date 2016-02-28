using UnityEngine;
using System.Collections;

public class DestroyBySide : MonoBehaviour {

	public GameObject explosion;

	void Start (){
		// Get reference to ShipController
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			// Call destroy method on ShipController, which checks if the player is orbiting an asteroid before destroying it
		} else {
			// Destroy asteroids that have gone off-screen
			Instantiate(explosion, other.transform.position, other.transform.rotation);
			Destroy(other.gameObject);
		}

	}
}
