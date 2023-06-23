using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteSpeedController : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private PostProcessVolume volume;
    private Vignette vignette;

    [SerializeField] private float maxVignetteIntensity;

    private void Start()
    {
        volume.profile.TryGetSettings(out Vignette v);
        vignette = v;
    }

    private void Update()
    {
        vignette.intensity.Override(Mathf.Lerp(0, maxVignetteIntensity, car.NormalizedLinearVelocity));
    }
}