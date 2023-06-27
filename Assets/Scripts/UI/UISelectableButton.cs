using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISelectableButton : UIButton
{
    [SerializeField] private Image selectionImage;

    public UnityEvent OnSelect, OnDeselect;

    private void Awake()
    {
        selectionImage.enabled = false;
    }
    public override void SetFocuse()
    {
        base.SetFocuse();

        selectionImage.enabled = true;

        OnSelect?.Invoke();
    }
    public override void SetUnFocuse()
    {
        base.SetUnFocuse();

        selectionImage.enabled = false;

        OnDeselect?.Invoke();
    }
}
