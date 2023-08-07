using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player;
    [SerializeField] private float minX, minY, maxX, maxY;

    private PhotonView view;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {


        if (view.IsMine)
        {
            byte group = 0;
            object[] data = new object[] { view.ViewID };

            Vector2 randomSpawnPos = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));

            PhotonNetwork.Instantiate(player.name, randomSpawnPos, Quaternion.identity, group, data);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Vector2 randomSpawnPos = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));

        PhotonNetwork.Instantiate(player.name, randomSpawnPos, Quaternion.identity);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
    }
}
