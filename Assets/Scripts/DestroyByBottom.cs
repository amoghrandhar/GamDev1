using UnityEngine;
using System.Collections;

public class DestroyByBottom : MonoBehaviour {

	void OnTriggerExit(Collider other)
	{
		// Destroy everything that goes off bottom of screen
		Destroy(other.gameObject);
	}

}
