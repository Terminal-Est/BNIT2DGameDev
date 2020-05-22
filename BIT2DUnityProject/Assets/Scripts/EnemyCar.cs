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

    public Transform player;
    public Transform[] patrolRoute;

    private float speed;
    private bool spotted = false;
    private int patrolDestPoint = 0;

    void Start()
    {
        player = GameObject.Find("PlayerVehicle").GetComponent<Transform>();
        circ = GetComponent<CircleCollider2D>();
        move = GetComponent<Transform>();
        rg2d = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        targetRotation = Quaternion.identity;
    }

    private void Update()
    {
          if (circ.IsTouching(GameObject.Find("PlayerVehicle").GetComponent<BoxCollider2D>()))
          {
            Debug.Log("Spotted");
            spotted = true;
          }
          if (spotted == true)
            Chase();
          else
          {
             Debug.Log("Searching");
             Patrol();
          }
    }

    void Chase()
    {
      targetPosition = GameObject.Find("PlayerVehicle").transform.position;
      //Added RotateTowards to make enemy car behave like player car
      targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition);
      move.rotation = Quaternion.RotateTowards(move.rotation, targetRotation, turnRate);
      rg2d.velocity = move.up * speed;
      if (box.IsTouching(GameObject.Find("PlayerVehicle").GetComponent<BoxCollider2D>()))
        StartCoroutine(WaitTime());
    }

    void Patrol()
    {
      speed = patrolSpeed;
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
    }
}
