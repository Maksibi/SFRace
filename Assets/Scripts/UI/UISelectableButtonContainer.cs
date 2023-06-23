using System;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class UISelectableButtonContainer : MonoBehaviour
{
    [SerializeField] private Transform buttonsContainer;

    public bool Interactable = true;
    public void SetInteractable(bool interactable) => Interactable = interactable;

    private UISelectableButton[] buttons;

    private int selectedButtonIndex = 0;

    private void Awake()
    {
        buttons = buttonsContainer.GetComponentsInChildren<UISelectableButton>();
    }

    private void OnEnable()
    {
        for (int i  = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter += OnPointerEnter;
        }

        if (!Interactable) return;

        buttons[selectedButtonIndex].SetFocuse();
    }

    private void OnDisable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter -= OnPointerEnter;
        }
    }

    private void OnPointerEnter(UIButton button)
    {
        SelectButton(button);
    }

    private void SelectButton(UIButton button)
    {
        if (!Interactable) return;

        buttons[selectedButtonIndex].SetUnFocuse();

        for(int i = 0; i < buttons.Length; i++)
        {
            if(button == buttons[i])
            {
                selectedButtonIndex = i;
                button.SetFocuse();
                break;
            }
        }
    }
    /*TODO
    public void SelectNext(){ }
    
    public void SelectPrevious() { }
    */
}
