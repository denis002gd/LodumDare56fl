using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAnt : MonoBehaviour
{
    private GameObject antEgg;
    private Transform eggSize;
    public GameObject ant;
    private float TimeToHatch = 2f;
    private float scaleFactor = 0.1f;
    private int stages;
    void Start()
    {
        eggSize = transform.GetChild(0);
       
    }

    // Update is called once per frame
    void Update()
    {
        TimeToHatch -= Time.deltaTime;
        if(TimeToHatch <= 0)
        {
            eggSize.transform.localScale = new Vector2(eggSize.transform.localScale.x + scaleFactor, eggSize.transform.localScale.y + scaleFactor);
            stages++;
            TimeToHatch = 2f;
        }
        if(stages >= 3)
        {
            eggSize.gameObject.SetActive(false);
            Instantiate(ant, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
