using UnityEngine;

public class WheelsController : MonoBehaviour
{
    [SerializeField]
    private float motorSpeed;
    [SerializeField]
    private float forwardWheelMultiplier;

    [SerializeField]
    private WheelJoint2D backWheel;
    [SerializeField]
    private WheelJoint2D forwardWheel;

    private void Update()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        if (horizontalAxis != 0)
        {
            JointMotor2D motor = new JointMotor2D()
            {
                motorSpeed = motorSpeed * -horizontalAxis,
                maxMotorTorque = 10000
            };
            backWheel.motor = motor;

            motor.motorSpeed *= forwardWheelMultiplier;
            forwardWheel.motor = motor;
        }
        else
        {
            backWheel.useMotor = false;
            forwardWheel.useMotor = false;
        }
    }
}
