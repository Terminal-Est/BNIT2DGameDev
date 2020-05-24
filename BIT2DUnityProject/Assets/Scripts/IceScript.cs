using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceScript : MonoBehaviour
{

   void Start()
   {

   }

   void Update()
   {
     Physics2D.IgnoreCollision(GetComponent<Collider2D>(), GetComponent<Collider2D>());
   }

}
