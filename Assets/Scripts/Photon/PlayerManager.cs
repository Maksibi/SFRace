using UnityEngine;
using Photon.Pun;
using System.IO;

[DisallowMultipleComponent]
public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private float minX, minY, maxX, maxY;

    private const string PHOTON_PREFABS_FOLDER = "PhotonPrefabs", PLAYER_CONTROLLER_PREFAB_NAME = "Player Car";

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (_photonView.IsMine)
        {
            CreatePlayerController();
        }
    }

    private void CreatePlayerController()
    {
        string prefabName = Path.Combine(PHOTON_PREFABS_FOLDER, PLAYER_CONTROLLER_PREFAB_NAME);
        byte group = 0;
        object[] data = new object[] { _photonView.ViewID };

        Vector2 randomSpawnPos = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));

        GameObject controller = PhotonNetwork.Instantiate(prefabName, randomSpawnPos, Quaternion.identity, group, data);
    }
}
