using UnityEngine;

public class SettingLoader : MonoBehaviour
{
    [SerializeField] private Setting[] allSettings;

    private void Start()
    {
        for (int i = 0; i < allSettings.Length; i++)
        {
            allSettings[i].Apply();
            allSettings[i].Load();
        }
    }
}
