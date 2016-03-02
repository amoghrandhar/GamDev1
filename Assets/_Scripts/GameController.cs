using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject asteroid;
    public Vector3 spawnValues;
	public AudioSource music;
	private int time;

    // Use this for initialization
    void Start () {
		music = GetComponent<AudioSource>();
		music.Play ();
        SpawnWaves();
		time = 0;
    }
	
	// Update is called once per frame
	void Update () {

		if (time > 250) {
		
			SpawnWaves ();
			Debug.Log ("hello");
			time = 0;
		
		}

		time++;
	
	}

    //This will create random asteroids at random position 
    void SpawnWaves() {
		
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
		Instantiate(asteroid, spawnPosition, spawnRotation);

		//AsteroidHandler ast = asteroid.GetComponent<AsteroidHandler>();
        //SpriteRenderer rendererCol = asteroid.GetComponentInChildren<SpriteRenderer>();
        //rendererCol.color = new Color(Random.value , Random.value , Random.value, 1f); // Set to opaque black


    }

}
