using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    #region Prefs
    [SerializeField] private WheelAxle[] wheelAxles;
    [SerializeField] private float wheelBaseLength;

    [SerializeField] private float downForceMin, downForceMax, downForceFactor;

    [SerializeField] private float angularDragMin, angularDragMax, angularDragFactor;

    [SerializeField] private Transform centerofMass;
    #endregion
    //DEBUG
    /*[SerializeField]*/
    public float engineTorque, brakeTorque, handBrakeTorque, steerAngle;

    public float LinearVelocity => rbody.velocity.magnitude * 3.6f;

    private Rigidbody rbody;

    public bool IsHanded, burnout;
    private void Start()
    {
        rbody = GetComponent<Rigidbody>();

        if (centerofMass != null)
        {
            rbody.centerOfMass = centerofMass.transform.localPosition;
        }

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].ConfigureVehicleSubsteps(50, 50, 50);
        }
    }
    private void FixedUpdate()
    {
        UpdateAngularDrag();

        UpdateDownForce();

        UpdateWheelAxles();
    }
    public float GetAverageRPM()
    {
        float sum = 0;

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            sum += wheelAxles[i].GetAverageRPM();
        }
        return sum / wheelAxles.Length;
    }
    public float GetWheelSpeed()
    {
        return GetAverageRPM() * wheelAxles[0].GetRadius() * 2 * 0.1885f;
    }
    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(downForceFactor * LinearVelocity, downForceMin, downForceMax);
        rbody.AddForce(-transform.up * downForce);
    }

    private void UpdateAngularDrag()
    {
        rbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
    }

    private void UpdateWheelAxles()
    {
        int motorWheelAmount = 0;

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].isHanded = this.IsHanded;
            wheelAxles[i].burnout = this.burnout;

            if (wheelAxles[i].IsMotor == true)
            {
                motorWheelAmount += 2;
            }
        }

        for (int i = 0; i < wheelAxles.Length; i++)
        {
            wheelAxles[i].Update();

            wheelAxles[i].ApplyMotorTorque(engineTorque / motorWheelAmount);
            wheelAxles[i].ApplySteerAngle(steerAngle, wheelBaseLength);
            wheelAxles[i].ApplyHandBrake(handBrakeTorque);
            wheelAxles[i].ApplyBrake(brakeTorque);
        }
    }
}
