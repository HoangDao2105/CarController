using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    float h;
    float v;
    bool isbreaking;
    Rigidbody rb;
    float steerAngle;
    [SerializeField] float motorForce;
    [SerializeField] float breakForce;
    [SerializeField] float maxSteerAngle;
    [SerializeField] WheelCollider wheel01;
    [SerializeField] WheelCollider wheel02;
    [SerializeField] WheelCollider wheel03;
    [SerializeField] WheelCollider wheel04;
    [SerializeField] Transform wheel01Trans;
    [SerializeField] Transform wheel02Trans;
    [SerializeField] Transform wheel03Trans;
    [SerializeField] Transform wheel04Trans;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1, 0);
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

    }
    void GetInput()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        isbreaking = Input.GetKey(KeyCode.Space);
    }
    void HandleMotor()
    {
        wheel01.motorTorque = v * motorForce;
        wheel02.motorTorque = v * motorForce;
        wheel03.motorTorque = v * motorForce;
        wheel04.motorTorque = v * motorForce;
        if (isbreaking)
        {
            wheel01.brakeTorque = breakForce;
            wheel02.brakeTorque = breakForce;
            wheel03.brakeTorque = breakForce;
            wheel04.brakeTorque = breakForce;
        }
        else
        {
            wheel01.brakeTorque = 0f;
            wheel02.brakeTorque = 0f;
            wheel03.brakeTorque = 0f;
            wheel04.brakeTorque = 0f;
        }
    }

    void HandleSteering()
    {
        steerAngle = maxSteerAngle * h;
        wheel01.steerAngle = steerAngle;
        wheel03.steerAngle = steerAngle;

    }
    void UpdateWheels()
    {
        UpdateSingleWheel(wheel01, wheel01Trans);
        UpdateSingleWheel(wheel02, wheel02Trans);
        UpdateSingleWheel(wheel03, wheel03Trans);
        UpdateSingleWheel(wheel04, wheel04Trans);
    }
    void UpdateSingleWheel(WheelCollider wheelcol,Transform wheeltrans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelcol.GetWorldPose(out pos, out rot);
        wheeltrans.position = pos;
        wheeltrans.rotation = rot;
    }
}
