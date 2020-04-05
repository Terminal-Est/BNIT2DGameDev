using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCar : MonoBehaviour
{
    public CarDrive car;

    private Func<Vector3> GetCameraFollowPositionFunc;
    public float cameraMoveSpeed;
    private float carVel;

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        CarZoom();
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;
        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        float cameraMoveSpeed = 10f;
        transform.position = transform.position + cameraMoveDir * cameraMoveSpeed * distance * Time.deltaTime;
    }

    private void CarZoom()
    {
        carVel = car.getCarVel();
        if (carVel > 3 && carVel < 7)
        {
          Camera.main.orthographicSize = carVel * 1.5f;
        }
        else if (carVel >= 7)
        {
          Camera.main.orthographicSize = 10.5f;
        }
    }
}
