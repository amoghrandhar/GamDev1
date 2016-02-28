using UnityEngine;
using System.Collections;
using UnityEditor;

public class Mover : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

	// Use this for initialization
	void Start ()
	{
	    rb = this.GetComponent<Rigidbody>();
	    rb.velocity = -1 * transform.forward*speed; // -ve as we want it to move down
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
