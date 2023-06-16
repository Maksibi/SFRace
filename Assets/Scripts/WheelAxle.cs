using System;
using UnityEngine;

[System.Serializable]
public class WheelAxle
{
    #region Prefs
    [SerializeField] private WheelCollider leftWheelCol, rightWheelCol;

    [SerializeField] private Transform leftWheel, rightWheel;

    [SerializeField] private bool isSteer, isMotor;
    public bool IsMotor => isMotor;
    public bool IsSteer => isSteer;

    public bool isHanded, burnout;

    [SerializeField] private float wheelWidth;

    [SerializeField] private float antiRollForce;

    [SerializeField] private float additionalWheelDownForce;

    [SerializeField] private float baseForwardStiffness = 1.5f;
    [SerializeField] private float stabilityForwardFactor = 1.0f;

    [SerializeField] private float baseSIdewaysStiffness = 2.0f;
    [SerializeField] private float stabilitySidewaysFactor = 1.0f;
    #endregion
    private WheelHit leftWheelHit, rightWheelHit;

    private float velocity = 0;
    #region Public API
    public void Update()
    {
        UpdateWheelHits();

        ApplyAntiRoll();
        ApplyDownForce();

        if (!isHanded) CorrectStiffness(false);

        SyncMeshTransform();
    }
    public void ConfigureVehicleSubsteps(float speedThreshold, int speedLowThreshold, int stepsAboveThreshold)
    {
        leftWheelCol.ConfigureVehicleSubsteps(speedThreshold, speedLowThreshold, stepsAboveThreshold);
        rightWheelCol.ConfigureVehicleSubsteps(speedThreshold, speedLowThreshold, stepsAboveThreshold);
    }
    public float GetAverageRPM()
    {
        return (leftWheelCol.rpm + rightWheelCol.rpm) * 0.5f;
    }
    public float GetRadius()
    {
        return leftWheelCol.radius;
    }
    public void ApplySteerAngle(float angle, float wheelBaseLength)
    {
        if (isSteer == false) return;

        float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(angle))));
        float angleSign = Mathf.Sign(angle);

        if (angle > 0)
        {
            leftWheelCol.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + wheelBaseLength / 2)) * angleSign;
            rightWheelCol.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - wheelBaseLength / 2)) * angleSign;
        }
        if (angle < 0)
        {
            rightWheelCol.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - wheelBaseLength / 2)) * angleSign;
            leftWheelCol.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + wheelBaseLength / 2)) * angleSign;
        }
        else
        {
            leftWheelCol.steerAngle = 0;
            rightWheelCol.steerAngle = 0;
        }

        leftWheelCol.steerAngle = angle;
        rightWheelCol.steerAngle = angle;
    }
    public void ApplyMotorTorque(float torque)
    {
        if (!isMotor) return;

        leftWheelCol.motorTorque = torque / 2;
        rightWheelCol.motorTorque = torque / 2;
    }
    public void ApplyHandBrake(float brake)
    {
        if (!isMotor) return;

        if (brake > 0)
        {
            leftWheelCol.motorTorque = 0;
            rightWheelCol.motorTorque = 0;

            leftWheelCol.brakeTorque = brake;
            rightWheelCol.brakeTorque = brake;

            CorrectStiffness(true);
        }
    }
    public void ApplyBrake(float brake)
    {
        if (!burnout)
        {
            leftWheelCol.brakeTorque = brake;
            rightWheelCol.brakeTorque = brake;
        }
        if (burnout)
        {
            if (IsMotor)
            {
                WheelFrictionCurve leftForward = leftWheelCol.forwardFriction;
                WheelFrictionCurve rightForward = rightWheelCol.forwardFriction;

                leftWheelCol.brakeTorque = 0.0f;
                rightWheelCol.brakeTorque = 0.0f;

                leftForward.stiffness = 0.0001f;
                rightForward.stiffness = 0.0001f;

                leftWheelCol.forwardFriction = leftForward;
                rightWheelCol.forwardFriction = rightForward;

                leftWheelCol.motorTorque = 2000;
                rightWheelCol.motorTorque = 2000;
            }
            else
            {
                leftWheelCol.brakeTorque = 2000;
                rightWheelCol.brakeTorque = 2000;

                leftWheelCol.brakeTorque = brake;
                rightWheelCol.brakeTorque = brake;
            }
        }
    }
    #endregion
    #region Private API
    private void UpdateWheelHits()
    {
        leftWheelCol.GetGroundHit(out leftWheelHit);
        rightWheelCol.GetGroundHit(out rightWheelHit);
    }
    private void CorrectStiffness(bool handbrake)
    {
        WheelFrictionCurve leftForward = leftWheelCol.forwardFriction;
        WheelFrictionCurve rightForward = rightWheelCol.forwardFriction;

        WheelFrictionCurve leftSideways = leftWheelCol.sidewaysFriction;
        WheelFrictionCurve rightSideways = rightWheelCol.sidewaysFriction;

        if (!handbrake)
        {
            leftForward.stiffness = baseForwardStiffness + Mathf.Abs(leftWheelHit.forwardSlip) * stabilityForwardFactor;
            rightForward.stiffness = baseForwardStiffness + Mathf.Abs(rightWheelHit.forwardSlip) * stabilityForwardFactor;

            leftSideways.stiffness = baseSIdewaysStiffness + Mathf.Abs(leftWheelHit.sidewaysSlip) * stabilitySidewaysFactor;
            rightSideways.stiffness = baseSIdewaysStiffness + Mathf.Abs(rightWheelHit.sidewaysSlip) * stabilitySidewaysFactor;

            leftWheelCol.forwardFriction = leftForward;
            rightWheelCol.forwardFriction = rightForward;

            leftWheelCol.sidewaysFriction = leftSideways;
            rightWheelCol.sidewaysFriction = rightSideways;
        }
        else
        {
            if (IsMotor)
            {
                leftForward.stiffness = Mathf.SmoothDamp(leftForward.stiffness, 0.4f, ref velocity, Time.deltaTime * 2);
                rightForward.stiffness = Mathf.SmoothDamp(rightForward.stiffness, 0.4f, ref velocity, Time.deltaTime * 2);

                leftWheelCol.forwardFriction = leftForward;
                rightWheelCol.forwardFriction = rightForward;

                leftSideways.stiffness = Mathf.SmoothDamp(leftSideways.stiffness, 0.4f, ref velocity, Time.deltaTime * 2);
                rightSideways.stiffness = Mathf.SmoothDamp(rightSideways.stiffness, 0.4f, ref velocity, Time.deltaTime * 2);

                leftWheelCol.sidewaysFriction = leftForward;
                rightWheelCol.sidewaysFriction = rightForward;
            }
            else
            {
                leftWheelCol.brakeTorque = 3000;
                rightWheelCol.brakeTorque = 3000;
            }
        }
    }
    private void ApplyDownForce()
    {
        if (leftWheelCol.isGrounded) leftWheelCol.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -additionalWheelDownForce *
            leftWheelCol.attachedRigidbody.velocity.magnitude, leftWheelCol.transform.position);

        if (rightWheelCol.isGrounded) rightWheelCol.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal * -additionalWheelDownForce *
            rightWheelCol.attachedRigidbody.velocity.magnitude, rightWheelCol.transform.position);
    }
    private void ApplyAntiRoll()
    {
        float travelL = 1.0f;
        float travelR = 1.0f;

        if (leftWheelCol.isGrounded == true)
        {
            travelL = (-leftWheelCol.transform.InverseTransformPoint(leftWheelHit.point).y - leftWheelCol.radius) /
                leftWheelCol.suspensionDistance;
        }
        if (rightWheelCol.isGrounded == true)
        {
            travelR = (-rightWheelCol.transform.InverseTransformPoint(rightWheelHit.point).y - rightWheelCol.radius) /
                rightWheelCol.suspensionDistance;
        }

        float forceDir = (travelL - travelR);

        if (leftWheelCol.isGrounded == true)
        {
            leftWheelCol.attachedRigidbody.AddForceAtPosition(leftWheelCol.transform.up * -forceDir *
                antiRollForce, leftWheelCol.transform.position);
        }
        if (rightWheelCol.isGrounded == true)
        {
            rightWheelCol.attachedRigidbody.AddForceAtPosition(rightWheelCol.transform.up * forceDir *
                antiRollForce, rightWheelCol.transform.position);
        }
    }
    private void SyncMeshTransform()
    {
        UpdateWheelTransform(leftWheelCol, leftWheel);
        UpdateWheelTransform(rightWheelCol, rightWheel);
    }
    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
#endregion