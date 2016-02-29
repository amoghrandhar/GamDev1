using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{

    public float tumble;
    public Rigidbody rb;
    public SphereCollider sc;


    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();

        transform.localScale = Vector3.one * Random.Range(1, 5);
        Vector2 vec2 = new Vector2(0, 1.0f);
        rb.angularVelocity = vec2 * tumble;
        sc = this.GetComponent<SphereCollider>();
        sc.radius = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
    }
}