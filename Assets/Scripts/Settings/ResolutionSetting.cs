using UnityEngine;

[CreateAssetMenu]
public class ResolutionSetting : Setting
{
    [SerializeField]
    private Vector2Int[] availableResolutions = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1280, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080)
    };
    private int currentResolutionIndex = 0;

    public override bool isMinValue { get => currentResolutionIndex == 0; }
    public override bool isMaxValue { get => currentResolutionIndex == availableResolutions.Length - 1; }

    public override void SetNextValue()
    {
        if (!isMaxValue) currentResolutionIndex++;
    }

    public override void SetPreviousValue()
    {
        if (!isMinValue) currentResolutionIndex--;
    }

    public override object GetValue()
    {
        return availableResolutions[currentResolutionIndex];
    }

    public override string GetStringValue()
    {
        return availableResolutions[currentResolutionIndex].x + "x" + availableResolutions[currentResolutionIndex].y;
    }

    public override void Apply()
    {
        Screen.SetResolution(availableResolutions[currentResolutionIndex].x, availableResolutions[currentResolutionIndex].y, true);

        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentResolutionIndex);
    }

    public override void Load()
    {
        currentResolutionIndex = PlayerPrefs.GetInt(title, 0);
    }
}
