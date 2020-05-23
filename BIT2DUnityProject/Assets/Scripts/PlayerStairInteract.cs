using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStairInteract : MonoBehaviour
{

    private EdgeCollider2D py;

    private void Start()
    {
        py = GetComponent<EdgeCollider2D>();    
    }

    private void FixedUpdate()
    {
        if (RunAndJump.axisUp == true)
        {
            py.enabled = true;
        } else
        {
            py.enabled = false;
        }
    }

}
