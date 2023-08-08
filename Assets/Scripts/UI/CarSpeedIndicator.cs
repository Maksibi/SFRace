using TMPro;
using UnityEngine;

public class CarSpeedIndicator : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        text.text = $"{3.6f * rb.velocity.magnitude:F0}";//("F0");
    }
}