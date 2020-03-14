using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAndJump: MonoBehaviour {

    private Animator animator;
    private Rigidbody2D rg2d;
    public float jumpForce;
    private int jumpCount;
    public int maxJumpCount;
    public float runSpeed;
    private int idle;
    private float moveDirection;



    // On GameObject awake  
    void Awake()
    {
        animator = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()

    {
        moveDirection = Input.GetAxis("Horizontal");
        rg2d.velocity = new Vector2(moveDirection * runSpeed, rg2d.velocity.y);

        if (rg2d.velocity.x > 0)
        {
            animator.SetInteger("personInt", 1);
            idle = 0;
        } else if (rg2d.velocity.x < 0)
        {
            animator.SetInteger("personInt", 2);
            idle = 3;
        }

        if (rg2d.velocity.x == 0)
        {
            animator.SetInteger("personInt", idle);
        }

        // For jump, check for spce key down 
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount )
        {
            rg2d.AddForce(new Vector2(rg2d.velocity.x, jumpForce), ForceMode2D.Impulse);
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
