using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private Transform move;
    private Quaternion targetRotation;
    private CircleCollider2D searchRadius;
    private BoxCollider2D box;
    private Transform target;
    private Vector3 targetPosition;

    private bool booped;

    public float speed;
    public float turnRate;
    public float nextWaypointDistance;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        searchRadius = GetComponent<CircleCollider2D>();
        box = GetComponent<BoxCollider2D>();
        targetRotation = Quaternion.identity;
        target = GameObject.Find("PlayerVehicle").GetComponent<Transform>();

        // if (seeker.IsDone())
        //   seeker.StartPath(rb.position, target.position, OnPathComplete);
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

    // Update is called once per frame
    void FixedUpdate()
    {

      // If there is no crash event with player, move enemy towards player
      if (searchRadius.IsTouching(GameObject.Find("PlayerVehicle").GetComponent<BoxCollider2D>()) && !booped)
      {
          //Added RotateTowards to make enemy car behave like player car
          targetPosition = target.transform.position;
          targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition - move.position);
          move.rotation = Quaternion.RotateTowards(move.rotation, targetRotation, turnRate);
          rb.velocity = move.up * speed;
      }

      //Check for collision, if enemy collides with player vehicle, set velocity to zero and call timer coroutine
      if (box.IsTouching(GameObject.Find("PlayerVehicle").GetComponent<BoxCollider2D>()) && !booped)
      {
          rb.velocity = new Vector2(0f, 0f);
          booped = true;
          StartCoroutine(Boop());
      }

      if (path == null)
        return;
      if (currentWaypoint >= path.vectorPath.Count)
      {
        reachedEndOfPath = true;
        return;
      }
      else
        reachedEndOfPath = false;

      Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;

      float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

      if (distance < nextWaypointDistance)
        currentWaypoint++;
    }



    IEnumerator Boop()
    {
        //yeild timer then set crash to false
        yield return new WaitForSeconds(1);
        print("Booped");
        booped = false;
    }
}
