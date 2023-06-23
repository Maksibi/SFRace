using UnityEngine;

[CreateAssetMenu]
public class RaceInfo : ScriptableObject
{
    [SerializeField] private string sceneName;
    //[SerializeField] private Sprite screenshot;
    [SerializeField] private string title;

    public string SceneName => sceneName;
    //public Sprite Screenshot => screenshot;
    public string Title => title;
}
