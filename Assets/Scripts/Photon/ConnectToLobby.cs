using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConnectToLobby : MonoBehaviourPunCallbacks
{
    private const string PHOTON_PREFABS_FOLDER = "PhotonPrefabs", PLAYER_MANAGER_PREFAB_NAME = "Player Manager";

    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.buildIndex == 2)
        {
            Debug.Log(scene.name + "Loaded");
            PhotonNetwork.Instantiate(Path.Combine(PHOTON_PREFABS_FOLDER, PLAYER_MANAGER_PREFAB_NAME), Vector3.zero, Quaternion.identity);
        }
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Multiplayer");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GameObject playerObject = FindPlayerObjectByPlayerId(otherPlayer.ActorNumber);
        if (playerObject != null)
        {
            PhotonNetwork.Destroy(playerObject);
        }
    }

    private GameObject FindPlayerObjectByPlayerId(int playerId)
    {
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject playerObject in playerObjects)
        {
            PhotonView photonView = playerObject.GetComponent<PhotonView>();

            if (photonView.Owner.ActorNumber == playerId)
                return playerObject;
        }
        return null;
    }
}