using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    #region Prefs
    [SerializeField] private Car car;

    [SerializeField] private float pitchModifier;
    [SerializeField] private float volumeModifier;
    [SerializeField] private float rpmModifier;

    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume1;
    [SerializeField] private float baseVolume2;

    [SerializeField] private AudioSource engineAudio1;
    [SerializeField] private AudioSource engineAudio2;
    #endregion
    private void Update()
    {
            engineAudio1.pitch = basePitch + pitchModifier * (car.EngineRPM / car.EngineMaxRPM * rpmModifier);
            engineAudio2.pitch = basePitch + pitchModifier * (car.EngineRPM / car.EngineMaxRPM * rpmModifier);
            engineAudio1.volume = baseVolume1 + volumeModifier * (car.EngineRPM / car.EngineMaxRPM);
            engineAudio2.volume = baseVolume2 + volumeModifier * (car.EngineRPM / car.EngineMaxRPM);
    }
}