using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AnimationCurve brakeCurve, steerCurve;

    [SerializeField][Range(0.0f, 1.0f)] private float autoBrakeFactor = 0.2f;

    private float wheelSpeed, verticalAxis, horizontalAxis, handBrakeAxis;

    private void Update()
    {
        //удалить
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

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
