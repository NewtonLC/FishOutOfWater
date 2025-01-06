using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Jump Variables
    public float jumpPower = 8f;
    public float jumpDuration = 0.3f;   // Time that player can hold the jump button
    public float flapPower = 6f;
    public int numFlaps = 1;
    private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isJumping = false;
    private float jumpTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update called once per frame
    void Update()
    {
        if (isGrounded){
            // Jump start
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                jumpTimeCounter = jumpDuration;
                Jump();
            }
        }
        else if (numFlaps > 0 && Input.GetButtonDown("Jump"))
        {
            numFlaps--;
            rb.velocity = new Vector2(rb.velocity.x, flapPower);
        }

        // Jump held
        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // Jump released
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            jumpTimeCounter = 0; 
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
            isJumping = false;
            numFlaps = 1;           //Set to player's max flaps
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ouch");
    }
}
