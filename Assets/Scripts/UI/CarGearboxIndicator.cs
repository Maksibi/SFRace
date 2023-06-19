using TMPro;
using UnityEngine;

public class CarGearboxIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private TextMeshProUGUI text;

    private void OnEnable()
    {
        car.GearChanged += OnGearChanged;
    }

    private void OnDisable()
    {
        car.GearChanged -= OnGearChanged;
    }

    private void OnGearChanged(string gearName)
    {
        text.text = gearName;

        if (car.HandBrakeControl == 1) text.text = "P";
    }
}