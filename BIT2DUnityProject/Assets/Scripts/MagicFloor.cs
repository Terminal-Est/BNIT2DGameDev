using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFloor : MonoBehaviour
{

    private void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void FixedUpdate()
    {
        if (RunAndJump.axisUp == true)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
