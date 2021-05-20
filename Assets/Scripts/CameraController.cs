using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    float xSum = 0;
    float ySum = 0;

    [SerializeField]
    private float CameraControlSpeed = 10f;

    //初始的旋转
    private Quaternion initRotation;
    private Quaternion initPlayerRotation;
    // Start is called before the first frame update
    void Start()
    {
        initPlayerRotation = transform.parent.rotation;
        initRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        float xCam = Input.GetAxis("Mouse X");

        float yCam = Input.GetAxis("Mouse Y");

        if (xCam != 0)
        {
            xSum += xCam;
        }
        if (yCam != 0)
        {
            ySum -= yCam;
        }

        //if(xSum>4)
        //    xSum = 4;
        //if(xSum<-4)
        //    xSum = -4;

        if (ySum > 2.5)
            ySum = 2.5f;
        if (ySum < -2.5)
            ySum = -2.5f;

        
        transform.parent.rotation = Quaternion.Euler(0, xSum * CameraControlSpeed, 0) * initPlayerRotation;
        transform.rotation = Quaternion.Euler(ySum * CameraControlSpeed, xSum * CameraControlSpeed, 0) * initRotation;

    }
}
