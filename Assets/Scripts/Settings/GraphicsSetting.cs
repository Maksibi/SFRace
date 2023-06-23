using UnityEngine;

[CreateAssetMenu]
public class GraphicsSetting : Setting
{
    private int currentLevelIndex = 0;

    public override bool isMinValue { get => currentLevelIndex == 0; }
    public override bool isMaxValue { get => currentLevelIndex == QualitySettings.names.Length - 1;}

    public override void SetNextValue()
    {
        if(isMaxValue == false)
        {
            currentLevelIndex++;
        }
    }

    public override void SetPreviousValue()
    {
        if (isMinValue == false)
        {
            currentLevelIndex--;
        }
    }

    public override object GetValue()
    {
        return QualitySettings.names[currentLevelIndex];
    }

    public override string GetStringValue()
    {
        return GetValue().ToString();
    }

    public override void Apply()
    {
        QualitySettings.SetQualityLevel(currentLevelIndex);
    }
}
