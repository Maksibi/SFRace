using UnityEngine;
using Photon.Pun;
using System.IO;
using Photon.Realtime;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class PlayerManager : MonoBehaviourPunCallbacks
{
    private const string PHOTON_PREFABS_FOLDER = "PhotonPrefabs", PLAYER_CONTROLLER_PREFAB_NAME = "Player Car",
        PLAYER_GUI_NAME = "PlayerRaceGUI";

    private const string SPAWN_POINTS_PARENT_NAME = "SpawnPoint";

    private PhotonView _photonView;

    private Transform startPoint;
    private List<Transform> spawnPoints;

    private int countOfPlayers;

    private void Awake()
    {
        spawnPoints = new List<Transform>();

        _photonView = GetComponent<PhotonView>();

        startPoint = GameObject.Find(SPAWN_POINTS_PARENT_NAME).transform;

        foreach (Transform t in startPoint)
        {
            spawnPoints.Add(t);
        }
    }

    private void Start()
    {
        if (_photonView.IsMine)
        {
            countOfPlayers = PhotonNetwork.CurrentRoom.PlayerCount;
            CreatePlayerController();
        }
    }

    private void CreatePlayerController()
    {
        string prefabName = Path.Combine(PHOTON_PREFABS_FOLDER, PLAYER_CONTROLLER_PREFAB_NAME);
        string GUIName = Path.Combine(PHOTON_PREFABS_FOLDER, PLAYER_GUI_NAME);

        byte group = 0;
        object[] data = new object[] { _photonView.ViewID };

        GameObject controller = PhotonNetwork.Instantiate(prefabName, spawnPoints[countOfPlayers - 1].position,
            spawnPoints[countOfPlayers - 1].rotation, group, data);
        GameObject GUI = PhotonNetwork.Instantiate(GUIName, Vector3.zero, Quaternion.identity, group, data);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        countOfPlayers++;
    }
    
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        countOfPlayers--;
        Destroy(gameObject);

        if (otherPlayer.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;

            foreach (Player player in PhotonNetwork.PlayerList)
                PhotonNetwork.CloseConnection(player);

            PhotonNetwork.LeaveRoom();
        }
    }
}
