using Photon.Pun;
using UnityEngine;
using Zenject;

public class MPRaceKeyboardStarter : MonoBehaviour
{
    private MPRaceStateTracker tracker;

    private void Awake()
    {
        tracker = FindObjectOfType<MPRaceStateTracker>();
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (Input.GetKeyDown(KeyCode.Return))
            tracker.LaunchCountdownTimer();
    }
}