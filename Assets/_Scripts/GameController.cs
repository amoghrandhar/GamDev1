using UnityEngine;
using UnityEngine.UI;
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

	// Score texts
	public Text player1score;
	public Text player2score;
	public Text player3score;
	public Text player4score;

    //Asteroids
    private AsteroidController asteroidHandler;

    public AudioSource music;
    private int deadPlayers;

    //Spwan Related Variables
    public Vector3 spawnValues;
    public float spawnWait, waveWait;
    public float asteroidCountToStartWith;
    private int asteroidCount;

	private int numberOfPlayers;

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
		numberOfPlayers = MenuScript.playerCount;

		if (numberOfPlayers < 4) {
			ship4.SetActive (false);
			player4score.text = "";
		}
		if (numberOfPlayers < 3) {
			ship3.SetActive (false);
			player3score.text = "";
		}
		if (numberOfPlayers < 2) {
			ship2.SetActive (false);
			player2score.text = "";
		}			

        asteroidHandler = asteroid.GetComponent<AsteroidController>();
        asteroidHandler.speed = 1.5f;
        deadPlayers = 0;
        asteroidCount = 0;
        StartCoroutine(startWaves());

    }

	void Update(){
		// Update scores of players
		if(numberOfPlayers >3) player4score.text = "P4 score: " + shipController4.getAsteroidCount();
		if(numberOfPlayers >2) player3score.text = "P3 score: " + shipController3.getAsteroidCount();
		if(numberOfPlayers >1) player2score.text = "P2 score: " + shipController2.getAsteroidCount();
		player1score.text = "P1 score: " + shipController.getAsteroidCount ();
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
