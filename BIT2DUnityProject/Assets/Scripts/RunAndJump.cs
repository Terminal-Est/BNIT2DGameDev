using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAndJump: MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rg2d;
    private PolygonCollider2D collide;
    public Vector2 jumpForce;
    private int jumpCount;
    public int maxJumpCount;
    public Vector2 runForce;
    private int persInt;
    private float vector;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
        collide = GetComponent<PolygonCollider2D>();
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // Check to see if right arrow is pressed
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Set vector positive (left) on x axis
            vector = 2.0f;
            // set force value of runForce Vector2 variable
            runForce = new Vector2(vector, 0.0f);
            // Set animation for left run
            if (persInt != 1)
            {
                animator.SetInteger("personInt", 1);
            }
            // Set Rigidbody2D vector and force mode to continuous 
            rg2d.AddForce(runForce, ForceMode2D.Force);
            // Limit velocity by adding force in opposite direction
            if (rg2d.velocity.x > 2)
            {
                rg2d.AddForce(new Vector2(-2.0f, 0.0f));
            }
        }
        // Everything as above for left arrow
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            vector = -2.0f;
            runForce = new Vector2(vector, 0.0f);
            if (persInt != 2)
            {
                animator.SetInteger("personInt", 2);
            }
            rg2d.AddForce(runForce, ForceMode2D.Force);
            if (rg2d.velocity.x < -2)
            {
                rg2d.AddForce(new Vector2(2.0f, 0.0f));
            }
        }
        // Set idle animation if character isn't moving
        else
        {
            persInt = animator.GetInteger("personInt");
            if (persInt == 1)
            {
                animator.SetInteger("personInt", 0);
            }
            else if (persInt == 2)
            {
                animator.SetInteger("personInt", 3);
            }


        }
        // For jump, check for spce key down 
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount )
        {
            // Check to make sure no direction is being pressed. This ensures the vector
            // is ser to zero for vertical jump
            if (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow))
            {
                vector = 0;
            }
            // Adds jumpForce to the Rigidbody2D component. Impulse for instantaneous
            // force only
            rg2d.AddForce(jumpForce, ForceMode2D.Impulse);
            // Increment jump counter for double jump
            jumpCount++;
        }
    }

    // Method for resetting jump counter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }
    }
}
