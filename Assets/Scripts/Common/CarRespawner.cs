using UnityEngine;
using Zenject;

public class CarRespawner : MonoBehaviour
{
    [SerializeField] private float respawnHeight;

    private RaceStateTracker _stateTracker;
    private Car _car;
    private CarInputControl _input;

    private TrackPoint respawnPoint;

    [Inject]
    public void Construct(RaceStateTracker stateTracker, Car car, CarInputControl input)
    {
        _stateTracker = stateTracker;
        _car = car;
        _input = input;
    }

    private void OnEnable()
    {
        _stateTracker.TrackpointPassed += OnTrackpointPassed;
    }

    private void OnDisable()
    {
        _stateTracker.TrackpointPassed -= OnTrackpointPassed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
            Respawn();
    }

    private void OnTrackpointPassed(TrackPoint point)
    {
        respawnPoint = point;
    }

    public void Respawn()
    {
        if (_stateTracker.State != RaceState.Race | respawnPoint == null)
            return;

        _car.Respawn(respawnPoint.transform.position + respawnPoint.transform.up * respawnHeight,
            respawnPoint.transform.rotation);

        _input.Reset();
    }
}
