using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatScript : MonoBehaviour
{
    [Header("CameraSettings")]
    public Camera cam;
    public Transform cameraPos;
    public float smoothSpeed;
    public Vector3 offset;
    private Vector3 desiredPos;
    public float zoomSpeed = 10f;
    public float minZoom = 4f;
    public float maxZoom = 15f;

    // New variable to set when cloud visibility should start
    public float cloudVisibilityStartZoom = 10f;

    [Header("BoatSettings")]
    public static Vector2 direction;
    public static float boatSpeed;
    private Rigidbody2D RbBoat;
    public float Decay;
    public GameObject ShootingLarva;
    public GameObject Wave;
    public float waveFreqency;
    private float org;

    // Clouds SpriteRenderer array
    public SpriteRenderer[] clouds;

    void Start()
    {
        RbBoat = GetComponent<Rigidbody2D>();
        org = waveFreqency - 2;
        if (cam == null)
        {
            cam = Camera.main;
        }
       
    }

    void Update()
    {
        
        
        // Handle zoom
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        cam.orthographicSize -= scrollInput * zoomSpeed;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);

        // Move the boat
        RbBoat.velocity = direction * boatSpeed;
        boatSpeed = Mathf.Lerp(boatSpeed, 0f, Time.deltaTime * Decay);
        if (Input.GetKey(KeyCode.LeftShift))
        {
           
            org -= Time.deltaTime;
            if (org < 0)
            {
                Instantiate(Wave, transform.position, Quaternion.identity);
                org = waveFreqency;
            }
        }
        AdjustCloudVisibility();
    }

    private void LateUpdate()
    {
       
        Vector3 desiredPosition = transform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        cameraPos.position = smoothedPosition;
    }


    void AdjustCloudVisibility()
    {
        float zoomLevel = cam.orthographicSize;

    
        if (zoomLevel < cloudVisibilityStartZoom)
        {
            foreach (SpriteRenderer cloud in clouds)
            {
                Color cloudColor = cloud.color;
                cloudColor.a = 0f; 
                cloud.color = cloudColor;
            }
        }
        else
        {
      
            float zoomFactor = Mathf.InverseLerp(cloudVisibilityStartZoom, maxZoom, zoomLevel);

   
            foreach (SpriteRenderer cloud in clouds)
            {
                Color cloudColor = cloud.color;
                cloudColor.a = Mathf.Lerp(0f, 1f, zoomFactor); 
                cloud.color = cloudColor;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Larva")
        {
            LarvaPickUp(collision.gameObject);
            Debug.Log("Touched", collision.gameObject);
        }
    }

    void LarvaPickUp(GameObject larva)
    {
        float angle = Random.Range(0f, 2 * Mathf.PI);

       
        float radius = Random.Range(0f, 2f);

        Vector2 spawnPosition = new Vector3(
                transform.position.x + Mathf.Cos(angle) * radius,
                transform.position.y + Mathf.Sin(angle) * radius,
                -5f
            );
        GameObject newLarva = Instantiate(ShootingLarva, spawnPosition, transform.rotation);
        newLarva.transform.SetParent(transform);
        UIScript.larvaCount++;
        Destroy(larva);
       
    }
}
