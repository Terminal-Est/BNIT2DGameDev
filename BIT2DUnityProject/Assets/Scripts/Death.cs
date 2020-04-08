using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour

{
    //public bool death = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Fall();

    }

    // private void Fall()
    // {
    //      var player = GameObject.FindGameObjectWithTag("Player");
    //      if (death)
    //      {
    //          //player.transform.position = new Vector2(-3.644f, -0.767f);
    //          FindObjectOfType<GameHandlerPlayer>().GameOver();
    //      }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<GameHandler>().GameOver();
        }
    }
}
