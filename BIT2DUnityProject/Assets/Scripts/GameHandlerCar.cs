using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandlerCar : MonoBehaviour
{

    public CameraFollowCar cameraFollow;
    public Transform playerTransform;

    void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }

}
