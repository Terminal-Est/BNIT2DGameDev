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
    private bool isGrounded;
    private float vector;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
        collide = GetComponent<PolygonCollider2D>();
        isGrounded = true;
        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.RightArrow))
        {
            vector = 2.0f;
            runForce = new Vector2(vector, 0.0f);
            if (persInt != 1)
            {
                animator.SetInteger("personInt", 1);
            }
            rg2d.AddForce(runForce, ForceMode2D.Force);
            if (rg2d.velocity.x > 2)
            {
                rg2d.AddForce(new Vector2(-2.0f, 0.0f));
            }
        }
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

        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount )
        {
            if (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow))
            {
                vector = 0;
            }
            //jumpForce = new Vector2(vector, 5.0f);
            rg2d.AddForce(jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
