using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public UnityEvent OnClick;

    public UnityAction<UIButton> PointerEnter;
    public UnityAction<UIButton> PointerExit;
    public UnityAction<UIButton> PointerClick;

    [SerializeField] protected bool Interactable;

    private bool isFocused = false;
    public bool IsFocused => isFocused;

    public virtual void SetFocuse()
    {
        if (!Interactable) return;

        isFocused = true;
    }

    public virtual void SetUnFocuse()
    {
        if (!Interactable) return;

        isFocused=false;
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!Interactable) return;

        PointerClick?.Invoke(this);
        OnClick?.Invoke();
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!Interactable) return;

        PointerEnter?.Invoke(this);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (!Interactable) return;

        PointerExit?.Invoke(this);
    }
}
