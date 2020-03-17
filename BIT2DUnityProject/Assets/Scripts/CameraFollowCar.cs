using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    public CarDrive car;
    private float carVel;
    private Func<Vector3> GetCameraFollowPositionFunc;
   
    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Zoom();
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;
        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 2f;
        transform.position = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

    }

    private void Zoom()
    {
        carVel = car.getCarVel();
        if (gameObject.tag == "MainCamera")
        {
            if (carVel > 3 ^ carVel < -3)
            {
                for (int i = 5; i <= 10; i++)
                {
                    Camera.main.orthographicSize = i;
                }

            }
            else
            {
                Camera.main.orthographicSize = 5;
            }
        }
    }
}
