using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> GearChanged;

    private CarChassis chassis;

    [SerializeField] private float maxSteerAngle, maxBrakeTorque, maxHandBrakeTorque;
    [SerializeField] private float maxSpeed;

    public float EngineRPM => engineRPM;
    public float EngineMaxRPM => engineMaxRPM;
    public float MaxSpeed => maxSpeed;
    public float ThrottleControl, SteerControl, BrakeControl, HandBrakeControl;
    public float LinearVelocity => chassis.LinearVelocity;
    public float WheelSpeed => chassis.GetWheelSpeed();

    public bool burnout;
    #region Prefs
    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float engineTorque, engineMaxTorque, engineRPM, engineMinRPM, engineMaxRPM;

    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;
    [SerializeField] private float upShiftEngineRPM;
    [SerializeField] private float downShiftEngineRPM;
    [SerializeField] private float reverseGear;
    [Header("Debug")]

    [SerializeField] private float selectedGear;
    [SerializeField] private int selectedGearIndex;
    #endregion
    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }
    private void FixedUpdate()
    {
        UpdateEngineTorque();
        AutoShiftGear();

        if (LinearVelocity >= maxSpeed) engineTorque = 0;

        chassis.engineTorque = engineTorque * ThrottleControl;
        chassis.steerAngle = maxSteerAngle * SteerControl;
        chassis.brakeTorque = maxBrakeTorque * BrakeControl;
        chassis.handBrakeTorque = maxHandBrakeTorque * HandBrakeControl;

        if(HandBrakeControl > 0) chassis.IsHanded = true;
        else chassis.IsHanded = false;

        chassis.burnout = this.burnout;
    }
    #region GEARBOX
    public string GetSelectedGearName()
    {
        if (selectedGear == reverseGear) return "R";

        if (selectedGear == 0) return "N";

        return (selectedGearIndex + 1).ToString();
    }
    private void AutoShiftGear()
    {
        if (selectedGear < 0) return;

        if (engineRPM >= upShiftEngineRPM && LinearVelocity > 30) UpGear();

        if (engineRPM < downShiftEngineRPM) DownGear();

        selectedGearIndex = Mathf.Clamp(selectedGearIndex, 0, gears.Length - 1);
    }
    public void UpGear()
    {
        ShiftGear(selectedGearIndex + 1);
    }
    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }
    public void ShiftToReverseGear()
    {
        if (burnout) return;
        selectedGear = reverseGear;
        GearChanged?.Invoke(GetSelectedGearName());
    }
    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }
    public void ShiftToNeutral()
    {
        selectedGear = 0;
        GearChanged?.Invoke(GetSelectedGearName());
    }
    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);

        selectedGear = gears[gearIndex];

        selectedGearIndex = gearIndex;

        GearChanged?.Invoke(GetSelectedGearName());
    }
    #endregion
    private void UpdateEngineTorque()
    {
        engineRPM = engineMinRPM + Mathf.Abs(chassis.GetAverageRPM() * selectedGear * finalDriveRatio);
        engineRPM = Mathf.Clamp(engineRPM, engineMinRPM, engineMaxRPM);

        engineTorque = engineTorqueCurve.Evaluate(engineRPM/engineMaxRPM) * engineMaxTorque * finalDriveRatio * Mathf.Sign(selectedGear);
    }
}
