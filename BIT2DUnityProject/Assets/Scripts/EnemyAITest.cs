using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAITest : MonoBehaviour
{
    private Transform move;
    private Transform target;
    private Quaternion targetRotation;
    private CircleCollider2D searchRadius;
    private BoxCollider2D box;
    private Vector3 targetPosition;

    private Transform player;
    public Transform[] patrolRoute;

    private bool booped;
    private bool spotted;
    private int patrolPoint = 0;

    public float speed;
    public float turnRate;
    public float nextWaypointDistance;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        booped = false;
        spotted = false;
        move = GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        searchRadius = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
        targetRotation = Quaternion.identity;
        player = GameObject.Find("PlayerVehicle").GetComponent<Transform>();

        InvokeRepeating("UpdatePath", 0f, 0.2f);
    }

    void UpdatePath()
    {

      if (seeker.IsDone())
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
      if (!p.error)
      {
        path = p;
        currentWaypoint = 0;
      }
    }

    void FixedUpdate()
    {
      if (spotted && !booped)
        Chase();
      else if (!spotted)
        Patrol();

      if (path == null)
        return;
      if (currentWaypoint >= path.vectorPath.Count)
        return;

      Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;
      float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

      if (distance < nextWaypointDistance)
        currentWaypoint++;
    }

    void Chase()
    {
      target = player; // change targetPosition to player;
      targetPosition = target.transform.position;
      targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition - move.position);
      move.rotation = Quaternion.RotateTowards(move.rotation, targetRotation, turnRate);
      rb.velocity = move.up * speed;

      if (box.IsTouching(GameObject.Find("PlayerVehicle").GetComponent<BoxCollider2D>()) && !booped)
      {
          Debug.Log("Crash Bang Wallop");
          rb.velocity = new Vector2(0f, 0f);
          booped = true;
          StartCoroutine(Boop());
      }
    }

    void Patrol()
    {
      target = patrolRoute[patrolPoint]; // change targetPosition to patrol route;

      targetPosition = target.transform.position;
      transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
      targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition - move.position);
      move.rotation = Quaternion.RotateTowards(move.rotation, targetRotation, turnRate);

      if (Vector2.Distance(transform.position, patrolRoute[patrolPoint].position) < 0.05f)
      {
          patrolPoint++;
          if (patrolPoint >= patrolRoute.Length)
            patrolPoint = 0;
      }
    }

    void OnTriggerEnter2D(Collider2D player)
    {
      if (player.name == "PlayerVehicle")
        spotted = true;
    }

    void OnTriggerExit2D(Collider2D player)
    {
      int closestPoint = 0;
      if (player.name == "PlayerVehicle")
      {
        spotted = false;
        for (int i = 0; i <= patrolPoint; i++)
        {
          if (Vector2.Distance(transform.position, patrolRoute[closestPoint].position) > Vector2.Distance(transform.position, patrolRoute[i].position))
            closestPoint = i;
        }
      }
    }

    IEnumerator Boop()
    {
        //yeild timer then set crash to false
        yield return new WaitForSeconds(0.8f);
        print("Booped");
        booped = false;
    }
}
