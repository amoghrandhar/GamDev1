using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

    public enum AsteroidType {
        FAST,
        NORMAL,
        SLOW
    };

	public AsteroidType asteroidType;
    public float tumble;
    public float speed;

	private Rigidbody rb;
    private SphereCollider sc;

    void Start() {
		
        rb = this.GetComponent<Rigidbody>();

		// Check that asteroid is not the earth
		if (this.tag != "Asteroid1") {
			// Give random size to asteroid
			transform.localScale = Vector3.one * Random.Range (2, 4);
			sc = this.GetComponent<SphereCollider>();
			sc.radius = 1.0f;
		}
        
		// Add rotation to asteroid
		Vector2 vec2 = new Vector2(0, 1.0f);
        rb.angularVelocity = vec2 * tumble;

    }
		
    void Update() {
		
		// Move asteroid
		transform.position += speed * Vector3.back * Time.deltaTime ;

    }

}