using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject prefabObj;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    // Start is called before the first frame update
    void Start()
    {
        platformWidth = prefabObj.GetComponent<BoxCollider2D>().size.x * prefabObj.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween, transform.position.y, transform.position.z);
            Vector3 halfExtents = new Vector3(platformWidth / 2, transform.position.y, transform.position.z);
            if(!Physics2D.OverlapBox(transform.position, halfExtents, transform.position.z))
            {
              Instantiate(prefabObj, transform.position, transform.rotation);
            }
        }
        if(transform.position.x > generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x - platformWidth - distanceBetween, transform.position.y, transform.position.z);
            Vector3 halfExtents = new Vector3(platformWidth / 2, transform.position.y, transform.position.z);
            if(!Physics2D.OverlapBox(transform.position, halfExtents, transform.position.z))
            {
              Instantiate(prefabObj, transform.position, transform.rotation);
            }
        }

    }
}
