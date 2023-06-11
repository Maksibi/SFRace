using UnityEngine;

public class GearboxEffects : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private ParticleSystem[] particle;
    [SerializeField] private Transform[] position;

    [SerializeField] private new AudioSource audio;

    private void OnEnable()
    {
        car.GearUpShifted += ApplyEffect;
    }
    private void OnDisable()
    {
        car.GearUpShifted -= ApplyEffect;
    }
    private void ApplyEffect()
    {
        for (int i = 0; i < particle.Length; i++)
        {
            particle[i].Emit(1);
            if(!audio.isPlaying) audio.Play();
        }
    }
}
