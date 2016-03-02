using UnityEngine;
using System.Collections;

public class DestroyByBottom : MonoBehaviour {

	public GameObject explosion;
	public AudioSource audio;

	private GameController gc;

	void Start() {
		audio = GetComponent<AudioSource>();
		gc = (GameController) GameObject.Find ("GameController").GetComponent<GameController> ();

	}

	void OnTriggerExit(Collider other){
		
		if (other.tag == "Player") {
			// Notify GameController that a player has died
			gc.PlayerDied ();

			//  Play sound
			Instantiate (explosion, other.transform.position, other.transform.rotation);
			audio.Play ();
		}

		// Destroy everything that goes off bottom of screen
		Destroy(other.gameObject);
	}

}
