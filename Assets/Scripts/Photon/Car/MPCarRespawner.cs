using Photon.Pun;
using UnityEngine;
using Zenject;

public class MPCarRespawner : MonoBehaviour
{
    [SerializeField] private float respawnHeight;

    private MPRaceStateTracker _stateTracker;
    private Car _car;
    private MPCarInput _input;
    private PhotonView view;

    private TrackPoint respawnPoint;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        _car = GetComponent<Car>();
        _input = GetComponent<MPCarInput>();
        _stateTracker = FindObjectOfType<MPRaceStateTracker>();
    }

    private void OnEnable()
    {
        if (view.IsMine)
            _stateTracker.TrackpointPassed += OnTrackpointPassed;
    }

    private void OnDisable()
    {
        if (view.IsMine)
            _stateTracker.TrackpointPassed -= OnTrackpointPassed;
    }

    private void Update()
    {
        if (view.IsMine)
            if (Input.GetKeyDown(KeyCode.R))
                Respawn();
    }

    private void OnTrackpointPassed(TrackPoint point)
    {
        if (view.IsMine)
            respawnPoint = point;
    }

    public void Respawn()
    {
        if (!view.IsMine)
            return;

        if (_stateTracker.State != MPRaceState.Race | respawnPoint == null)
            return;

        _car.Respawn(respawnPoint.transform.position + respawnPoint.transform.up * respawnHeight,
            respawnPoint.transform.rotation);

        _input.Reset();
    }
}
