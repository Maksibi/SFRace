using UnityEngine;
using Photon.Pun;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerManagerCreator : MonoBehaviourPunCallbacks
{
    public static PlayerManagerCreator instance;

    private const string PHOTON_PREFABS_FOLDER = "PhotonPrefabs", PLAYER_MANAGER_PREFAB_NAME = "Player Manager";

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

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
        if (scene.buildIndex == 3)
        {
            Debug.Log(scene.name + "Loaded");
            PhotonNetwork.Instantiate(Path.Combine(PHOTON_PREFABS_FOLDER, PLAYER_MANAGER_PREFAB_NAME), Vector3.zero, Quaternion.identity);
        }
    }
}
