using TMPro;
using UnityEngine;

public class CarSpeedIndicator : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        text.text = car.LinearVelocity.ToString("F0");
    }
}
