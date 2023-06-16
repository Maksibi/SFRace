using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour
{
    [SerializeField] private RaceStateTracker tracker;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            tracker.LaunchCountdownTimer();
        }
    }
}
