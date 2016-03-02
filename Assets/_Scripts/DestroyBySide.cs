using UnityEngine;
using System.Collections;

public class DestroyBySide : MonoBehaviour {

	public GameObject explosion;
	public AudioSource audio;

	void Start() {
		audio = GetComponent<AudioSource>();
	}

	void OnTriggerExit(Collider other){
		
		if (other.tag == "Player") {

			GameObject player = other.gameObject;
			ShipController playerController = player.GetComponent<ShipController> ();

			if (playerController.isReleased()) {
				Destroy (other.gameObject);
				Instantiate (explosion, other.transform.position, other.transform.rotation);
				if(other.tag == "Player")
					audio.Play ();
			}

		}

	}
}
