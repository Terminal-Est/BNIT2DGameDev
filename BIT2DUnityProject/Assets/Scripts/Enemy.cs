using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  private bool facingRight = true;
  private int patrolDestPoint = 0;

  public float speed;
  public bool reloading = false;
  public float agroRange;
  private Animator animator;

  public Transform projectile;
  public Transform castPoint;
  public Transform firePoint;
  public Transform player;
  public Transform[] patrolRoute;
    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponent<Animator>();
      projectile = GameObject.Find("Bullet").GetComponent<Transform>();
      player = GameObject.Find("Player").GetComponent<Transform>();
      castPoint = GameObject.Find("CastPoint").GetComponent<Transform>();
      firePoint = GameObject.Find("CastPoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      if(CanSeePlayer(agroRange))
        Shoot();
      else
        Patrol();
    }

    void Shoot()
    {
      animator.SetInteger("enemyBehaviour", 1);
      if (!reloading)
      {
        Instantiate(projectile, firePoint.position, transform.rotation);
        reloading = true;
        StartCoroutine(Reload());
      }
    }

    void Patrol()
    {
      transform.position = Vector2.MoveTowards(transform.position, patrolRoute[patrolDestPoint].position, speed * Time.deltaTime);
      animator.SetInteger("enemyBehaviour", 0);
      if (Vector2.Distance(transform.position, patrolRoute[patrolDestPoint].position) < 0.2f)
      {
          patrolDestPoint++;
          if (patrolDestPoint >= patrolRoute.Length)
            patrolDestPoint = 0;
      }
      if (patrolDestPoint == 1 && !facingRight)
      {
        transform.Rotate(0f, 180f, 0f);
        facingRight = true;
      }
      else if (patrolDestPoint == 0 && facingRight)
      {
        transform.Rotate(0f, 180f, 0f);
        facingRight = false;
      }
    }

    bool CanSeePlayer(float distance)
    {
      bool spotted = false;
      float castDist = distance;

      if (!facingRight)
      {
        castDist = -distance;
      }

      Vector2 lineOfSight = castPoint.position + Vector3.right * castDist;

      RaycastHit2D hit = Physics2D.Linecast(castPoint.position, lineOfSight, 1 << LayerMask.NameToLayer("Action"));

      if (hit.collider != null)
      {
        if (hit.collider.gameObject.CompareTag("Player"))
          spotted = true;
        else
          spotted = false;
        Debug.DrawLine(castPoint.position, hit.point, Color.red);
      }
      else
        Debug.DrawLine(castPoint.position, lineOfSight, Color.green);
      return spotted;
    }

    IEnumerator WaitTime()
    {
      yield return new WaitForSeconds(4);
    }

    IEnumerator Reload()
    {
      yield return new WaitForSeconds(0.583f);
      reloading = false;
    }
}
