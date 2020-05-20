using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  private bool facingRight = true;
  private bool reloading;
  private int patrolDestPoint = 0;

  public float speed;
  private Animator animator;
  public Transform player;
  public Transform[] patrolRoute;
    // Start is called before the first frame update
    void Start()
    {
      animator = GetComponent<Animator>();
      reloading = false;
      player = GameObject.Find("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      Patrol();
    }

    void Shoot()
    {
      animator.SetInteger("enemyBehaviour", 1);
      if (reloading == false)
        Debug.Log("Pew Pew");
      else
        StartCoroutine(ReloadTime());
    }

    void Patrol()
    {
      transform.position = Vector2.MoveTowards(transform.position, patrolRoute[patrolDestPoint].position, speed * Time.deltaTime);
      animator.SetInteger("enemyBehaviour", 0);
      if (Vector2.Distance(transform.position, patrolRoute[patrolDestPoint].position) < 0.2f)
      {
          StartCoroutine(WaitTime());
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

    IEnumerator WaitTime()
    {
      yield return new WaitForSeconds(4);
    }

    IEnumerator ReloadTime()
    {
      yield return new WaitForSeconds(2);
      reloading = false;
    }
}
