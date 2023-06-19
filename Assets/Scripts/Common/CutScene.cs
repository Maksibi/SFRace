using UnityEngine;
using UnityEngine.Playables;
using Zenject;

public class CutScene : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    private RaceStateTracker _stateTracker;

    [Inject]
    public void Construct(RaceStateTracker raceStateTracker)
    {
        _stateTracker = raceStateTracker;
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        
    }
    private void Update()
    {
        if (_stateTracker.State != RaceState.Preparation) return;

        if (Input.GetKeyUp(KeyCode.Return))
        {
            director.time = 27;
        }
    }
}
