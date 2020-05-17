using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCar : MonoBehaviour
{

    private CircleCollider2D circ;
    private BoxCollider2D box;
    private Transform move;
    private Rigidbody2D rg2d;
    private GameObject target;
    private Quaternion targetRotation;
    private bool booped;
    public float turnRate;
    public float speed;

    void Start()
    {
        circ = GetComponent<CircleCollider2D>();
        move = GetComponent<Transform>();
        rg2d = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        target = GameObject.Find("PlayerVehicle");
        targetRotation = Quaternion.identity;
        booped = false;
    }

    private void Update()
    {
        // Ig there is no crash event with player, move enemy towards player
        if (circ.IsTouching(GameObject.Find("PlayerVehicle").GetComponent<BoxCollider2D>()) && !booped)
        {
         //Added RotateTowards to make enemy car behave like player car
            Vector3 targetPosition = target.transform.position;
            targetRotation = Quaternion.LookRotation(Vector3.forward, targetPosition - move.position);
            move.rotation = Quaternion.RotateTowards(move.rotation, targetRotation, turnRate);
            rg2d.velocity = move.up * speed;
        }

        //Check for collision, if enemy collides with player vehicle, set velocity to zero and call timer coroutine
        if (box.IsTouching(GameObject.Find("PlayerVehicle").GetComponent<BoxCollider2D>()) && !booped)
        {
            rg2d.velocity = new Vector2(0f, 0f);
            booped = true;
            StartCoroutine(Boop());
        }
    }

    IEnumerator Boop()
    {
        //yeild timer then set crash to false
        yield return new WaitForSeconds(1);
        print("Booped");
        booped = false;
    }
}
