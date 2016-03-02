using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public GameObject asteroid;

    //Ships
    public GameObject ship;
    private ShipController shipController;
    public GameObject ship2;
    private ShipController shipController2;
	public GameObject ship3;
	private ShipController shipController3;
	public GameObject ship4;
	private ShipController shipController4;

    //Asteroids
    private AsteroidController asteroidHandler;

    public AudioSource music;
    private int deadPlayers;

    //Spwan Related Variables
    public Vector3 spawnValues;
    public float spawnWait, waveWait;
    public float asteroidCountToStartWith;
    private int asteroidCount;

    // Use this for initialization
    void Start()
    {
        music = GetComponent<AudioSource>();
        music.Play();

		//Get ship controller instances
        shipController = ship.GetComponent<ShipController>();
        shipController2 = ship2.GetComponent<ShipController>();
		shipController3 = ship3.GetComponent<ShipController>();
		shipController4 = ship4.GetComponent<ShipController>();


		// Initalise game for correct number of players
		if(MenuScript.playerCount <4) ship4.SetActive(false);
		if(MenuScript.playerCount <3) ship3.SetActive(false);
		if(MenuScript.playerCount <2) ship2.SetActive(false);

			

        asteroidHandler = asteroid.GetComponent<AsteroidController>();
        asteroidHandler.speed = 1.5f;
        deadPlayers = 0;
        asteroidCount = 0;
        StartCoroutine(startWaves());

    }

	void Update(){

	}

    IEnumerator startWaves()
    {
        yield return new WaitForSeconds(0);

        while (true)
        {
			// Start spawning waves when a ship has reached the initial asteroid
            if (shipController.getAsteroidCount() > 0 || 
				shipController2.getAsteroidCount() > 0 || 
				shipController3.getAsteroidCount() > 0 || 
				shipController4.getAsteroidCount() > 0 )
            {
                StartCoroutine(SpawnWaves());
                break;
            }
            yield return new WaitForSeconds(0);
        }
        yield return new WaitForSeconds(0);
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(0);
        while (true)
        {

            if (asteroidCount % 4 == 0)
            {
                asteroidHandler.speed += 0.5f;
                shipController.asteroidBoost += 20;
                spawnWait -= 0.1f;
            }

            for (int i = 0; i < asteroidCountToStartWith; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(asteroid, spawnPosition, spawnRotation);
                asteroidCount++;
                yield return new WaitForSeconds(spawnWait);

            }
            yield return new WaitForSeconds(waveWait);
        }
    }


    public void PlayedDied()
    {
        deadPlayers++;
    }

}
