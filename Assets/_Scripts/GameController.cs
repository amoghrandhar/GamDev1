/*
 * Author: Amogh Randhar, Daniel Cao, Kwong Chi Tam
 */

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
	public Image player1ship, player2ship, player3ship, player4ship;

	// Game text
	public Text gameOverTitle, gameOverText;

    //Asteroids
	private AsteroidController asteroidHandler, fastAsteroidController, slowAsteroidController;

    public AudioSource music;
    private int deadPlayers;

    //Spawn Related Variables
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
			player4ship.enabled = false;
		}
		if (numberOfPlayers < 3) {
			ship3.SetActive (false);
			player3score.text = "";
			player3ship.enabled = false;
		}
		if (numberOfPlayers < 2) {
			ship2.SetActive (false);
			player2score.text = "";
			player2ship.enabled = false;
		}			

		// Initialise game state
		gameOver = false;
		gameStart = false;
		gameOverTitle.text = "";
		gameOverText.text = "";
		deadPlayers = 0;
		asteroidCount = 0;

		// Get references for asteroid controllers
        asteroidHandler = asteroid.GetComponent<AsteroidController>();
		fastAsteroidController = fastAsteroid.GetComponent<AsteroidController> ();
		slowAsteroidController = slowAsteroid.GetComponent<AsteroidController> ();

		// Set initial speeds of asteroids
        asteroidHandler.speed = 1f;
		fastAsteroidController.speed = 1.2f;
		slowAsteroidController.speed = 0.8f;

        StartCoroutine(StartWaves());

    }

	void Update(){

		if (!gameOver) {
			
			// Update scores of players
			if (numberOfPlayers > 3) {
				player4score.text = "P4 (J) score: " + shipController4.getAsteroidCount ();
				player4ship.enabled = true;
			}
			if (numberOfPlayers > 2) {
				player3score.text = "P3 (A) score: " + shipController3.getAsteroidCount ();
				player3ship.enabled = true;
			}
			if (numberOfPlayers > 1) {
				player2score.text = "P2 (F) score: " + shipController2.getAsteroidCount ();
				player2ship.enabled = true;
			}
			player1score.text = "P1 (L. Click) score: " + shipController.getAsteroidCount ();
			player1ship.enabled = true;

			// Check if the game is over
			if (deadPlayers >= numberOfPlayers) {
				gameOver = true;
				gameOverTitle.text = "Game Over !";
				StartCoroutine(BlinkText ());
			}

		} else {
			
			if (Input.GetKey ("escape"))
				SceneManager.LoadScene ("Menu");

			if (Input.GetKey ("r"))
				SceneManager.LoadScene ("OrbitScene");
			
		}
	}

	public IEnumerator BlinkText() {
		while (gameOver) {
			gameOverText.text = "Press 'R' to restart \n\nPress 'Esc' to go back to the menu";
			yield return new WaitForSeconds (.7f);
			gameOverText.text = "";
			yield return new WaitForSeconds (.5f);
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
            if (asteroidCount % 5 == 0) {
                asteroidHandler.speed += 0.8f;
				fastAsteroidController.speed += 0.8f;
				slowAsteroidController.speed += 0.4f;
                shipController.asteroidBoost += 20;
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

			// For every 5 asteroids spawned, spawn special asteroids with a random chance
			if (asteroidCount > 8 && asteroidCount % 4 == 0) {
				if(Random.value < 0.5) createSlowAsteroid();
			}

			if (asteroidCount > 12 && asteroidCount % 4 == 0) {
				if(Random.value >= 0.5) createFastAsteroid ();
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
