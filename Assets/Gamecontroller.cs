using UnityEngine;
using System.Collections;

public class Gamecontroller : MonoBehaviour {

    public GameObject asteroid;
    public Vector3 spawnValues;

    // Use this for initialization
    void Start () {
        SpawnWaves();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //This will create random asteroids at random position 
    void SpawnWaves()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
        Quaternion spawnRotation = Quaternion.identity;
        Instantiate(asteroid, spawnPosition, spawnRotation);
        Mover ast = asteroid.GetComponent<Mover>();
        ast.speed = 5;
        SpriteRenderer rendererCol = asteroid.GetComponentInChildren<SpriteRenderer>();
        rendererCol.color = new Color(Random.value , Random.value , Random.value, 1f); // Set to opaque black


    }

}
