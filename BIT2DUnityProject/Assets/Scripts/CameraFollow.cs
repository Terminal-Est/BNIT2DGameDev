using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public CarDrive car;

    public float cameraMoveSpeed;
    private float carVel;
    private Func<Vector3> GetCameraFollowPositionFunc;

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
      Scene currentScene = SceneManager.GetActiveScene();
      string sceneName = currentScene.name;
      if (sceneName == "Level1Car")
      {
        CarZoom();
        cameraMoveSpeed = 10f;
      }
      else if (sceneName == "Level1Player")
      {
        cameraMoveSpeed = 5f;
      }
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;
        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);
        transform.position = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

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
