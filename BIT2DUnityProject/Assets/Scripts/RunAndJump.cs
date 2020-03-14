using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAndJump: MonoBehaviour {

    public int maxJumpCount;
    public float runSpeed;
    public float jumpForce;

    private Animator animator;
    private Rigidbody2D rg2d;
    private int jumpCount;
    private float moveDirection;
    private bool facingRight = true;



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
        Animate();
        // For jump, check for spce key down
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount )
        {
            rg2d.AddForce(new Vector2(rg2d.velocity.x, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void Animate(){
      if (moveDirection * runSpeed != 0)
      {
        animator.SetInteger("personInt", 1);
        if(moveDirection > 0 && !facingRight)
        {
          FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
          FlipCharacter();
        }
      }
      else
      {
        animator.SetInteger("personInt", 0);
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

    private void FlipCharacter(){
      facingRight = !facingRight; //inverse
      transform.Rotate(0f, 180f, 0f);
    }
}
