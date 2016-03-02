using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject asteroid;
	public GameObject ship;
	private AsteroidController asteroidHandler;
	private ShipController shipController;

    public Vector3 spawnValues;
	public AudioSource music;

	private int time;
	private int deadPlayers;

    // Use this for initialization
    void Start () {
		music = GetComponent<AudioSource>();
		music.Play ();
		shipController = ship.GetComponent<ShipController>();
		asteroidHandler = asteroid.GetComponent<AsteroidController> ();
		asteroidHandler.speed = 1.5f;
		time = 250;
		deadPlayers = 0;
    }
	
	// Update is called once per frame
	void Update () {

		if (shipController.getAsteroidCount() > 0) {

			if (time > 1000) {
				asteroidHandler.speed = 4;
				shipController.asteroidBoost = 180;
			}

			if (time % 250 == 0) {
				SpawnWaves ();
			}

			time++;

		}
	
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

	public void PlayedDied(){
		deadPlayers++;
	}

}
