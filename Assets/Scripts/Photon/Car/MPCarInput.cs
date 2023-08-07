using System;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class MPCarInput : MonoBehaviour
{
    [SerializeField] private AnimationCurve brakeCurve, steerCurve;

    [SerializeField][Range(0.0f, 1.0f)] private float autoBrakeFactor = 0.2f;

    private float wheelSpeed, verticalAxis, horizontalAxis, handBrakeAxis;

    private Car car;
    private PhotonView view;
    private MPCarInput input;
    private CinemachineFreeLook _camera;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        car = GetComponent<Car>();
        input = GetComponent<MPCarInput>();
        _camera = GetComponentInChildren<CinemachineFreeLook>();

        if (!view.IsMine)
        {
            input.enabled= false;
            _camera.enabled = false;
            car.enabled = false;
        }
    }

    private void Update()
    {
        if (!view.IsMine) return;

        if (Input.GetKeyDown(KeyCode.E)) car.UpGear();
        if (Input.GetKeyDown(KeyCode.Q)) car.DownGear();
        //
        wheelSpeed = car.WheelSpeed;

        UpdateAxis();
        UpdateThrottleAndBrake();
        Burnout();
        UpdateSteer();
        UpdateAutoBrake();
    }

    public void Stop()
    {
        if (!view.IsMine) return;

        Reset();

        car.BrakeControl = 1;
    }

    public void Reset()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handBrakeAxis = 0;

        car.ThrottleControl = 0;
        car.SteerControl = 0;
        car.BrakeControl = 0;
        car.HandBrakeControl = 0;
    }

    #region Private API
    private void UpdateThrottleAndBrake()
    {
        car.HandBrakeControl = handBrakeAxis;

        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottleControl = Mathf.Abs(verticalAxis);
            car.BrakeControl = 0;
        }
        else
        {
            car.BrakeControl = brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed);
            car.ThrottleControl = 0;
        }

        if(verticalAxis < 0 & wheelSpeed > -0.5f & wheelSpeed <= 0.5f)
        {
            car.ShiftToReverseGear();
        }
        if(verticalAxis > 0 & wheelSpeed > -0.5f & wheelSpeed < 0.5f)
        {
            car.ShiftToFirstGear();
        }
    }

    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * horizontalAxis;
    }

    private void UpdateAutoBrake()
    {
        if (Input.GetAxis("Vertical") == 0)
        {
            car.BrakeControl = (brakeCurve.Evaluate(wheelSpeed / car.MaxSpeed)) * autoBrakeFactor;
        }
    }

    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handBrakeAxis = Input.GetAxis("Jump");
    }

    private void Burnout()
    {
        car.burnout = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S);

        if (!car.burnout) return;
    }
}
#endregion