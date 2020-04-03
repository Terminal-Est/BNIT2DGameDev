using UnityEngine;
 using System.Collections;

 public class NewCarDriveTest : MonoBehaviour {

     float xspeep = 0f;
     float power = 0.001f;
     float friction = 0.95f;
     bool right = false;
     bool left = false;

     public float fuel = 2;


     // Use this for initialization
     void FixedUpdate () {


         if(right){
             xspeep += power;
             fuel -= power;
         }
         if(left){
             xspeep -= power;
             fuel -= power;
         }


     }

     // Update is called once per frame
     void Update () {

         if(Input.GetKeyDown("w")){
             right = true;
         }
         if(Input.GetKeyUp("w")){
             right = false;
         }
         if(Input.GetKeyDown("s")){
             left = true;
         }
         if(Input.GetKeyUp("s")){
             left = false;
         }

         if(fuel < 0){

             xspeep = 0;

         }

         xspeep *= friction;
         transform.Translate(Vector3.right * -xspeep);

     }
 }
