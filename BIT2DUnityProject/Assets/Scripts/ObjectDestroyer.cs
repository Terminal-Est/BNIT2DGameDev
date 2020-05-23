using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public GameObject RightObjectDestructionPoint;
    public GameObject LeftObjectDestructionPoint;

    private float platformWidth;
    // Start is called before the first frame update
    void Start()
    {
        RightObjectDestructionPoint = GameObject.Find("RightObjectDestructionPoint");
        LeftObjectDestructionPoint = GameObject.Find("LeftObjectDestructionPoint");
        platformWidth = gameObject.GetComponent<BoxCollider2D>().size.x * gameObject.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < RightObjectDestructionPoint.transform.position.x - platformWidth)
        {
            Destroy(gameObject);
        }
        if (transform.position.x > LeftObjectDestructionPoint.transform.position.x + platformWidth)
        {
            Destroy(gameObject);
        }
    }
}
