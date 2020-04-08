﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class CarDrive: MonoBehaviour {

    public float driveSpeed;
    public float turnRate;
    public float fuel;

    private Rigidbody2D rg2d;
    private float driveSpeedRetain;
    private float moveX;
    private float moveY;
    private float steeringRightAngle;
    private float driftForce;
    private float carVelocity;
    private Vector2 rightAngleFromForward;
    private Vector2 relativeForce;
    public Vector3 move;

    // Quaternions are used to represent rotations.
    Quaternion targetRotation;

    // On GameObject awake
    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        targetRotation = Quaternion.identity;
        driveSpeedRetain = driveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Brake();
    }

    // Fixed update is used for physics calls, this can be more or less than the frame rate
    private void FixedUpdate()
    {
        carVelocity = rg2d.velocity.magnitude;
        Steer();
    }

    // Brake method used to set car speed to zero on jump axis equaling 1
    private void Brake()
    {
        if (Input.GetAxis("Jump") == 1.0f)
        {
            driveSpeed = 0.0f;
        }
        else
        {
            driveSpeed = driveSpeedRetain;
        }
    }

    // Steer method, used to control the X,Y,Z movement of the car
    private void Steer()
    {
        // Assigns x and y move to horizontal and verticle axis
        // Create new Vector3(x,y,z) with these axis
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        move = new Vector3(moveX, moveY, 0);

        // Check to see if either x or y axis are greater than zero
        if ((move.x != 0.0f || move.y != 0.0f) && fuel > 0)
        {
            // If the above is true, look towards Vector3.forward and take into account the move value
            if (move.y < 0)
            {
              targetRotation = Quaternion.LookRotation(Vector3.forward, move);
            }
            targetRotation = Quaternion.LookRotation(Vector3.forward, move);
            // To prescribe an arc while moving useing RotateTowards. We use the current rotation as the start point
            // and the above targetRotation as the end point
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnRate);
            // Add force to accelerate in the direction we have specified, we take the current vector3.up
            // value, the driveSpeed variable and our fixed FixedUpdate delta (time between updates)
            // and multiply them together to get an x, y force.
            rg2d.AddRelativeForce(Vector3.up * driveSpeed * Time.fixedDeltaTime);
            fuel -= driveSpeed / 10000;
            // Calculate drift
            // Get a right angle compared to the current rotational velocity
            if (rg2d.angularVelocity > 0)
            {
                steeringRightAngle = -90;
            }
            else
            {
                steeringRightAngle = 90;
            }
            // Find a vector2 that is 90 degrees relative to the local forward direction
            rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * Vector2.up;
            // Calculate the drift sideways velocity from comparing rigidbody forward movement and the
            // right angle to this
            driftForce = Vector2.Dot(rg2d.velocity*2, rg2d.GetRelativeVector(rightAngleFromForward.normalized));
            // Apply an opposite force from the drift direction to simulate tire grip
            relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);
            rg2d.AddForce(rg2d.GetRelativeVector(relativeForce));
        }
    }

    public float getCarVel()
    {
      return carVelocity;
    }

    // This method will load platform section on collision with goal
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DriveGoal")
        {
            SceneManager.LoadScene("Transition");
        }
        else if (collision.gameObject.tag == "Block")
        {
              PlayerStats.playerHealth -= 5;
              PlayerStats.vehicleDamage += 5;
              print("Vehicle has " + PlayerStats.vehicleDamage + " damage.");
              print("Player has " + PlayerStats.playerHealth + " damage.");
        }
    }

}
