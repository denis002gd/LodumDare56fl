using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    private Transform Object;
    public float speed;
    private Vector2 direction;
    private Rigidbody rb;
    void Start()
    {
        direction = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
