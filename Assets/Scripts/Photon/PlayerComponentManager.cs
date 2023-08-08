using Cinemachine;
using Photon.Pun;
using UnityEngine;

public class PlayerComponentManager : MonoBehaviour
{
    private Car car;
    private PhotonView view;
    private MPCarInput input;
    private CinemachineFreeLook _camera;
    private CarChassis chassis;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        car = GetComponent<Car>();
        chassis = GetComponent<CarChassis>();
        _camera = GetComponentInChildren<CinemachineFreeLook>();
        input = GetComponent<MPCarInput>();
    }

    private void Start()
    {
        if (!view.IsMine)
        {
            car.enabled = false;
            chassis.enabled = false;
            _camera.enabled = false;
            input.enabled = false;
        }
    }
}
