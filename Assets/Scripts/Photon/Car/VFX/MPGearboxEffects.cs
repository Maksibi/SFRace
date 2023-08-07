using UnityEngine;
using Photon.Pun;

public class MPGearboxEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particle;
    [SerializeField] private Transform[] position;

    [SerializeField] private new AudioSource audio;

    private Car car;
    private PhotonView view;

    private void Awake()
    {
        view = GetComponentInParent<PhotonView>();

        if (!view.IsMine) return;

        car = GetComponentInParent<Car>();
    }

    private void OnEnable()
    {
        if (!view.IsMine) return;
        car.GearUpShifted += ApplyEffect;
    }

    private void OnDisable()
    {
        car.GearUpShifted -= ApplyEffect;
    }

    private void ApplyEffect()
    {
        if (!view.IsMine) return;

        for (int i = 0; i < particle.Length; i++)
        {
            particle[i].Emit(1);
            if (!audio.isPlaying) audio.Play();
        }
    }
}