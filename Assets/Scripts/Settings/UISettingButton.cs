using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISettingButton : UISelectableButton
{
    [SerializeField] private Setting setting;

    [SerializeField] private TextMeshProUGUI titleText, valueText;

    [SerializeField] private UIButton nextButton, previousButton;

    public void SetNextValueSetting() 
    {
        setting?.SetNextValue();
        setting?.Apply();
        UpdateInfo();
    }
    public void SetPreviousValueSetting()
    {
        setting?.SetPreviousValue();
        setting?.Apply();
        UpdateInfo();
    }

    private void Start()
    {
        ApplyProperty(setting);
    }

    private void UpdateInfo()
    {
        titleText.text = setting.Title;
        valueText.text = setting.GetStringValue();

        previousButton.enabled = !setting.isMinValue;
        nextButton.enabled = !setting.isMaxValue;
    }

    public void ApplyProperty(Setting property)
    {
        if (!property) return;

        setting = property;

        UpdateInfo();
    }
}
