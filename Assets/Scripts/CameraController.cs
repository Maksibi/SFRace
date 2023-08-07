using Cinemachine;
using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    #region Prefs

    [SerializeField] private new CinemachineFreeLook camera;
    [SerializeField] private CinemachineCameraOffset offset;

    [SerializeField] private float baseFOV, minFOV, maxFOV;
    [SerializeField] private float baseOffsetZ, minOffsetZ, maxOffsetZ;

    [SerializeField][Range(0f, 1f)] private float normalizedSpeedShake;
    [SerializeField] private float shakeAmount;

    #endregion
    private Car car;

    private void Awake()
    {
        car = GetComponentInParent<Car>();
    }

    private void Start()
    {
        camera.m_Lens.FieldOfView = baseFOV;
        offset.m_Offset.z = baseOffsetZ;
    }

    private void Update()
    {
        camera.m_Lens.FieldOfView = Mathf.Lerp(minFOV, maxFOV, car.NormalizedLinearVelocity);
        offset.m_Offset.z = Mathf.Lerp(minOffsetZ, maxOffsetZ, car.NormalizedLinearVelocity);

        if (car.NormalizedLinearVelocity >= normalizedSpeedShake)
        {
            offset.m_Offset.x = Random.insideUnitSphere.x * shakeAmount * Time.deltaTime;
            offset.m_Offset.y = Random.insideUnitSphere.y * shakeAmount * Time.deltaTime;
        }
    }
}