using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

    public float tumble;
    public float speed;
	private Rigidbody rb;
    private SphereCollider sc;


    // Use this for initialization
    void Start() {
		
        rb = this.GetComponent<Rigidbody>();

        transform.localScale = Vector3.one * Random.Range(2, 4);
        Vector2 vec2 = new Vector2(0, 1.0f);
        rb.angularVelocity = vec2 * tumble;
        sc = this.GetComponent<SphereCollider>();
        sc.radius = 1.0f;

    }

    // Update is called once per frame
    void Update() {
		// Move asteroid
		transform.position += speed * Vector3.back * Time.deltaTime ;

    }

}