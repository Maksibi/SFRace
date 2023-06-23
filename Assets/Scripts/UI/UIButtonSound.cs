using System;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound, selectSound;

    private AudioSource _audio;

    private UIButton[] buttons;

    private void Awake()
    {
        _audio = GetComponent<AudioSource> ();
        buttons = GetComponentsInChildren<UIButton>(true);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter += OnPointerEnter;
            buttons[i].PointerClick += OnPointerClick;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter -= OnPointerEnter;
            buttons[i].PointerClick -= OnPointerClick;
        }
    }

    private void OnPointerClick(UIButton arg0)
    {
        _audio.PlayOneShot(clickSound);
    }

    private void OnPointerEnter(UIButton arg0)
    {
        _audio.PlayOneShot(selectSound);
    }
}
