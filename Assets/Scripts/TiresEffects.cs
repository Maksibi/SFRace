using UnityEngine;

public class TiresEffects : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private ParticleSystem[] wheelSmokes;

    [SerializeField] private new AudioSource audio;

    [SerializeField] private GameObject skidPrefab;

    [SerializeField] float forwardSlipLimit, sidewaysSlipLimit;

    [SerializeField] private float SkidYOffset;

    private WheelHit wheelHit;

    private Transform[] skidTrail;

    private void Start()
    {
        skidTrail = new Transform[wheelColliders.Length];
    }
    private void Update()
    {
        bool isSlip = false;

        for(int i = 0; i < wheelColliders.Length; i++)
        {
            wheelColliders[i].GetGroundHit(out wheelHit);

            if (wheelColliders[i].isGrounded)
            {
                if (wheelHit.forwardSlip > forwardSlipLimit || wheelHit.sidewaysSlip > sidewaysSlipLimit)
                {
                    if (skidTrail[i] == null) skidTrail[i] = Instantiate(skidPrefab).transform;

                    if (!audio.isPlaying) audio.Play();

                    if (skidTrail[i] != null)
                    {
                        Vector3 pos = new Vector3();
                        pos.x += wheelHit.point.x;
                        pos.y += wheelHit.point.y + SkidYOffset;
                        pos.z += wheelHit.point.z;
                        skidTrail[i].position = pos;
                        skidTrail[i].forward = -wheelHit.normal;

                        wheelSmokes[i].transform.position = skidTrail[i].position;
                        wheelSmokes[i].Emit(10);
                    }
                    isSlip = true;

                    continue;
                }
            }
            skidTrail[i] = null;
            wheelSmokes[i].Stop();
        }
        if (!isSlip) audio.Stop();
    }
}
