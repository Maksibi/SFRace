using Photon.Pun;
using UnityEngine;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void OnConnectedToServer()
    {
        Debug.Log("Connected to server");
    }
}
