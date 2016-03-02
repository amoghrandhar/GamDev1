using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour {

    public GameObject asteroid , slowAsteroid , fastAsteroid;

    //Ships
	public GameObject ship, ship2, ship3, ship4;
	private ShipController shipController, shipController2, shipController3, shipController4;

	// Score texts
	public Text player1score, player2score, player3score, player4score;

	// Game text
	public Text gameOverText;

    //Asteroids
	private AsteroidController asteroidHandler, fastAsteroidController, slowAsteroidController;

    public AudioSource music;
    private int deadPlayers;

    //Spwan Related Variables
    public Vector3 spawnValues;
    public float spawnWait, waveWait;
    public float asteroidCountToStartWith;
    private int asteroidCount;

	private int numberOfPlayers;
	private bool gameStart, gameOver;

    void Start() {

		// Play music
        music = GetComponent<AudioSource>();
        music.Play();

		// Get ship controller instances
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

		// Initialise game state
		gameOver = false;
		gameStart = false;
		gameOverText.text = "";
		deadPlayers = 0;
		asteroidCount = 0;

		// Get references for asteroid controllers
        asteroidHandler = asteroid.GetComponent<AsteroidController>();
		fastAsteroidController = fastAsteroid.GetComponent<AsteroidController> ();
		slowAsteroidController = slowAsteroid.GetComponent<AsteroidController> ();

		// Set initial speeds of asteroids
        asteroidHandler.speed = 1.5f;
		fastAsteroidController.speed = 2f;
		slowAsteroidController.speed = 1f;

        StartCoroutine(StartWaves());

    }

	void Update(){

		if (!gameOver) {
			
			// Update scores of players
			if (numberOfPlayers > 3)
				player4score.text = "P4 score: " + shipController4.getAsteroidCount ();
			if (numberOfPlayers > 2)
				player3score.text = "P3 score: " + shipController3.getAsteroidCount ();
			if (numberOfPlayers > 1)
				player2score.text = "P2 score: " + shipController2.getAsteroidCount ();
			player1score.text = "P1 score: " + shipController.getAsteroidCount ();

			// Check if the game is over
			if (deadPlayers >= numberOfPlayers) {
				gameOver = true;
				gameOverText.text = "Game over!\nPress 'R' to restart or 'Esc' to go back to the menu";		
			}

		} else {
			
			if (Input.GetKey ("escape"))
				SceneManager.LoadScene ("Menu");

			if (Input.GetKey ("r"))
				SceneManager.LoadScene ("OrbitScene");
			
		}
	}

	// Start spawning asteroids once spaceships have launched
    IEnumerator StartWaves() {
		
        yield return new WaitForSeconds(0);

        while (true){
			
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

	// Spawn asteroids and modify speeds
    IEnumerator SpawnWaves() {
		
        yield return new WaitForSeconds(0);
		while (!gameOver) {

			// For every 4 asteroids spawned, increase speed
            if (asteroidCount % 4 == 0) {
                asteroidHandler.speed += 1.2f;
				fastAsteroidController.speed += 1.6f;
				slowAsteroidController.speed += 0.5f;
                shipController.asteroidBoost += 30;
                spawnWait -= 0.1f;
				waveWait -= 0.1f;
            }

			// Spawn asteroids in random positions
            for (int i = 0; i < asteroidCountToStartWith; i++) {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(asteroid, spawnPosition, spawnRotation);
                asteroidCount++;
                yield return new WaitForSeconds(spawnWait);
            }

			// For every 4 asteroids spawned, spawn special asteroids with a random chance
			if (asteroidCount % 4 == 0) {
				if(Random.value >= 0.5) createFastAsteroid();
				if(Random.value < 0.5) createSlowAsteroid();
			}

            yield return new WaitForSeconds(waveWait);

        }
    }

    private void createFastAsteroid() {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(fastAsteroid, spawnPosition, spawnRotation);
        asteroidCount++;
    }

    private void createSlowAsteroid() {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(slowAsteroid, spawnPosition, spawnRotation);
        asteroidCount++;
    }

    public void PlayerDied() {
        deadPlayers++;
    }

	public bool isStarted () {
		return gameStart;
	}

	public void startGame () {
		gameStart = true;
	}

}
