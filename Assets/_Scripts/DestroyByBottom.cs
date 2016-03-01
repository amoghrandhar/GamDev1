using UnityEngine;
using System.Collections;

public class DestroyByBottom : MonoBehaviour {

	public GameObject explosion;

	void OnTriggerExit(Collider other)
	{
		// Destroy everything that goes off bottom of screen
		Instantiate(explosion, other.transform.position, other.transform.rotation);
		Destroy(other.gameObject);
	}

}
