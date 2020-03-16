using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathzoneGenerator : MonoBehaviour
{

    public GameObject deathZone;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;
    // Start is called before the first frame update
    void Start()
    {
        platformWidth = deathZone.GetComponent<BoxCollider2D>().size.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.z);
            Instantiate(deathZone, transform.position, transform.rotation);
        }

    }
}
