using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool canMove = true;
    public float boatSpeedIncrease;
    public float Decay;
    public float IncreaseSpeed;
    public float playerSize;
    public Transform BoatLocation;
    public float maxSpeed;
    private bool BoatMoves;
    public Animator animator;
    public GameObject midPoint;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);

        if (movement.magnitude > 1)
        {
            movement.Normalize();
        }
        if (horizontal != 0f || vertical != 0f)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }

        if (canMove)
        {
            Vector2 boatVelocity = BoatLocation.GetComponent<Rigidbody2D>().velocity;
            rb.velocity = boatVelocity + movement * playerSpeed;

            if (Input.GetKeyDown(KeyCode.A))
            {
                sprite.flipX = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                sprite.flipX = false;
            }
        }

        Vector3 directionToPlayer = transform.position - midPoint.transform.position;
        float distance = directionToPlayer.magnitude;


        Vector3 oppositeDirection = -directionToPlayer.normalized;


        Vector2 angleVector = new Vector2(oppositeDirection.x, oppositeDirection.y);


        if (Input.GetKey(KeyCode.LeftShift) && distance >= 1.8f)
        {
            BoatScript.direction = angleVector;
            BoatScript.boatSpeed = distance * boatSpeedIncrease;
        }
    }
}
