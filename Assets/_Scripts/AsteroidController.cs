using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour
{

    public enum AsteroidType
    {
        FAST,
        NORMAL,
        SLOW
    };

    public float tumble;
    public float speed;
	private Rigidbody rb;
    private SphereCollider sc;

    public AsteroidType asteroidType;

    // Use this for initialization
    void Start() {
		
        rb = this.GetComponent<Rigidbody>();

		if (this.tag != "Asteroid1") {
			transform.localScale = Vector3.one * Random.Range (2, 4);
			sc = this.GetComponent<SphereCollider>();
			sc.radius = 1.0f;
		}
        
		Vector2 vec2 = new Vector2(0, 1.0f);
        rb.angularVelocity = vec2 * tumble;

    }

    // Update is called once per frame
    void Update() {
		// Move asteroid
		transform.position += speed * Vector3.back * Time.deltaTime ;

    }

}