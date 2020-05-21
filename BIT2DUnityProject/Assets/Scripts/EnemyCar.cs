using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{

    private CircleCollider2D circ;
    private BoxCollider2D box;
    private Transform move;
    private Rigidbody2D rg2d;
    private Quaternion targetRotation;
    private Vector3 targetPosition;

    public float turnRate;
    public float patrolSpeed;
    public float chaseSpeed;
    private float actualVelocity;
    private float maxVelocity = 6;

    public Transform player;
    public Transform[] patrolRoute;

    private float speed;
    private bool crash;
    private int patrolDestPoint = 0;

    void Start()
    {
        crash = false;
        player = GameObject.Find("PlayerVehicle").GetComponent<Transform>();
        circ = GetComponent<CircleCollider2D>();
        move = GetComponent<Transform>();
        rg2d = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        targetRotation = Quaternion.identity;
    }

    private void Update()
    {
          actualVelocity = rg2d.velocity.magnitude;
          if (circ.IsTouching(player.GetComponent<BoxCollider2D>()) && crash == false)
            Chase();
          else
            Patrol();
    }

    void Chase()
    {
      if (crash == false)
        speed = chaseSpeed;
      targetPosition = player.transform.position;
      //Added RotateTowards to make enemy car behave like player car
      targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition);
      move.rotation = Quaternion.RotateTowards(move.rotation, targetRotation, turnRate);
      rg2d.velocity = move.up * speed;
      if (box.IsTouching(player.GetComponent<BoxCollider2D>()))
      {
        crash = true;
        Debug.Log("Crashed");
        StartCoroutine(WaitTime());
      }
    }

    void Patrol()
    {
      speed = patrolSpeed;
      if (actualVelocity > maxVelocity)
      {
        speed = maxVelocity;
        rg2d.velocity = move.up * speed;
      }
      transform.position = Vector2.MoveTowards(transform.position, patrolRoute[patrolDestPoint].position, speed * Time.deltaTime);
      targetRotation = Quaternion.LookRotation(Vector3.forward, patrolRoute[patrolDestPoint].position - move.position);
      move.rotation = Quaternion.RotateTowards(move.rotation, targetRotation, turnRate);
      if (Vector2.Distance(transform.position, patrolRoute[patrolDestPoint].position) < 0.2f)
      {
          patrolDestPoint++;
          if (patrolDestPoint >= patrolRoute.Length)
            patrolDestPoint = 0;
      }
    }

    IEnumerator WaitTime()
    {
      speed = 0;
      yield return new WaitForSeconds(2);
      crash = false;
    }
}
