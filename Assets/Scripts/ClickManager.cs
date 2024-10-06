using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public static Vector2 clickPosition; // To store the position of the click
    public GameObject clickIndicator;

    // Update is called once per frame
    void Update()
    {
 
        if (Input.GetMouseButtonDown(0)) 
        {
 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

    
            if (hit.collider != null)
            {

                Debug.Log("Clicked on: " + hit.collider.gameObject.name);

                
                clickPosition = hit.point;
                Instantiate(clickIndicator, clickPosition, Quaternion.identity);
                Debug.Log("Click Position: " + clickPosition);

                
            }
        }
    }
}
