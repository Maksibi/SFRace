using UnityEngine;

public class SpeedWindEffect : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private new AudioSource audio;

    [SerializeField] private float minSpeedLimit, baseVolume, speedVolumeMultiplier;

    private void Update()
    {
        if (car.LinearVelocity >= minSpeedLimit)
        {
            particle.Emit(1);

            speedVolumeMultiplier = baseVolume * car.NormalizedLinearVelocity;
            audio.volume = baseVolume * speedVolumeMultiplier;
            if (!audio.isPlaying) audio.Play();
        }
        if (car.LinearVelocity <= minSpeedLimit & audio.isPlaying) audio.Stop();
    }
}