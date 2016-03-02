using UnityEngine;
using System.Collections;

public class DestroyByBottom : MonoBehaviour {

	public GameObject explosion;
	public AudioSource audio;

	void Start() {
		audio = GetComponent<AudioSource>();
	}

	void OnTriggerExit(Collider other){
		// Destroy everything that goes off bottom of screen
		Destroy(other.gameObject);
		if (other.tag == "Player") {
			Instantiate (explosion, other.transform.position, other.transform.rotation);
			audio.Play ();
		}
	}

}
