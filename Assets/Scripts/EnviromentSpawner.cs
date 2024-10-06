using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentSpawner : MonoBehaviour
{
    public GameObject[] dropObjects;
    public GameObject[] enemyObjects;
    public int rarityObj;
    public int rarityEnemy;
    public float spawnRate;
    private float timer;
    public float spawnRange;
    void Start()
    {
        timer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timer >= 0f)
        {
            timer -= Time.deltaTime;
        }
        else
        {
           SpawnObjects();
           
            timer = spawnRate;
        }
    
    }
    void SpawnObjects()
    {
        for (int i = 0; i < rarityObj; i++)
        {
            // Generate a random angle in radians
            float angle = Random.Range(0f, 2 * Mathf.PI);

            // Calculate random radius within the specified spawn range
            float radius = Random.Range(spawnRate, spawnRange);

         
            Vector2 spawnPosition = new Vector2(
                transform.position.x + Mathf.Cos(angle) * radius,
                transform.position.y + Mathf.Sin(angle) * radius
            );

           
            Instantiate(dropObjects[Random.Range(0, dropObjects.Length)], spawnPosition, transform.rotation);
        }
    }

}
