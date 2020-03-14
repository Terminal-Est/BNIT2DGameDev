using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour

{

    bool death;

    // Start is called before the first frame update
    void Start()
    {
        death = false;
    }

    // Update is called once per frame
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (death)
        {
            player.transform.position = new Vector3(-2.089f, 0.046f, 0.0f);
            death = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            death = true;
            print("dead");
        }
    }
}