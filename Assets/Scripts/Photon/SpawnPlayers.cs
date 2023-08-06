using Photon.Pun;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float minX, minY, maxX, maxY;

    private void Start()
    {
        Vector2 randomSpawnPos = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));

        PhotonNetwork.Instantiate(player.name, randomSpawnPos, Quaternion.identity);
    }
}
