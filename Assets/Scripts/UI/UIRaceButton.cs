using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIRaceButton : UISelectableButton
{
    [SerializeField] private RaceInfo raceInfo;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI title;

    private void Start()
    {
        ApplyProperty(raceInfo);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (!raceInfo) return;
        SceneManager.LoadScene(raceInfo.SceneName);
    }

    public void ApplyProperty(RaceInfo property)
    {
        if (!property) return;

        raceInfo = property;

        //image.sprite = raceInfo.Screenshot;
        title.text = raceInfo.Title;
    }
}
