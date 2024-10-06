using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntMovementAI : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPosition;
    private bool canMove = true;
    private SpriteRenderer spriteRenderer;
    private Transform boatTransform;
    private Vector3 lastBoatPosition;
    private Vector3 targetRelativeToBoat;
   
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boatTransform = GameObject.FindWithTag("Boat").transform;
        lastBoatPosition = boatTransform.position; // Store the boat's initial world position
        targetPosition = transform.position; // Set the initial target position
        targetRelativeToBoat = transform.position - boatTransform.position; // Initial target relative to boat
    }

    void Update()
    {
        // Calculate the boat's movement since the last frame
        Vector3 boatMovement = boatTransform.position - lastBoatPosition;

        // Apply the same movement to the ant
        transform.position += boatMovement;

        // Also update the target position based on the boat's movement
        targetPosition += boatMovement;

        if (Input.GetMouseButtonDown(0))
        {
            speed = 2f;

            // Convert the mouse screen position to world position
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition = new Vector2(mousePosition.x + Random.Range(-1f, 1f), mousePosition.y + Random.Range(-1f,1f));
            // Calculate the target position relative to the boat, convert mousePosition to Vector3
            targetRelativeToBoat = (Vector3)mousePosition - boatTransform.position;

            // Set the world target position based on the boat’s current position + relative target
            targetPosition = boatTransform.position + targetRelativeToBoat;
            
            // Flip sprite based on the direction
            if (mousePosition.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }

        if (canMove)
        {
            
            MoveTowardsTarget();
        }
       

        // Update last boat position for the next frame
        lastBoatPosition = boatTransform.position;
    }

    void MoveTowardsTarget()
    {
        // Move towards the target position in world space
        Vector3 direction = (targetPosition - transform.position).normalized;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("LeafLim") || collision.collider.CompareTag("Player"))
        {
            speed = 0f;
          
        }
    }
}
