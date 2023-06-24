using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerFloatSettings : Setting
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string parameterName;

    [SerializeField] private float virtualStep, minRealValue, maxRealValue;
    [SerializeField] private float minVirtualValue, maxVirtualValue;

    private float currentValue = 0;

    public override bool isMinValue { get => currentValue == minRealValue; }
    public override bool isMaxValue { get => currentValue ==  maxRealValue; }

    public override void SetNextValue()
    {
        AddValue(Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override void SetPreviousValue()
    {
        AddValue(-Mathf.Abs(maxRealValue - minRealValue) / virtualStep);
    }

    public override string GetStringValue()
    {
        return Mathf.Lerp(minVirtualValue, maxVirtualValue, (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
    }

    public override object GetValue()
    {
        return currentValue;
    }

    public override void Apply()
    {
        audioMixer.SetFloat(parameterName, currentValue);
    }

    public override void Load()
    {
        currentValue = PlayerPrefs.GetFloat(title, 0);
    }

    private void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);

        Save();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(title, currentValue);
    }
}
