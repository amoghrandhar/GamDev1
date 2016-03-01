using UnityEngine;
using System.Collections;

public class AsteroidHandler : MonoBehaviour
{
    public float speed;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame -> Here we are moving the asteroid and rotating as well
    void Update()
    {
		transform.position += -speed*Vector3.forward * Time.deltaTime ;
    }
}