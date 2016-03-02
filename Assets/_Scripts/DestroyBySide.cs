using UnityEngine;
using System.Collections;

public class DestroyBySide : MonoBehaviour {

	public GameObject explosion;
	public AudioSource audio;

	private GameController gc;

	void Start() {
		audio = GetComponent<AudioSource>();
		gc = (GameController) GameObject.Find ("GameController").GetComponent<GameController> ();
	}

	void OnTriggerExit(Collider other){
		
		if (other.tag == "Player") {

			GameObject player = other.gameObject;
			ShipController playerController = player.GetComponent<ShipController> ();

			if (playerController.isReleased()) {
				gc.PlayerDied ();
				Instantiate (explosion, other.transform.position, other.transform.rotation);
				if(other.tag == "Player")
					audio.Play ();
				Destroy (other.gameObject);

			}

		}

	}
}
