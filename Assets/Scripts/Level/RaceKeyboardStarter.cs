using UnityEngine;
using Zenject;

public class RaceKeyboardStarter : MonoBehaviour
{
    private RaceStateTracker tracker;

    [Inject]
    public void Construct(RaceStateTracker tracker) => this.tracker = tracker;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            tracker.LaunchCountdownTimer();
        }
    }
}