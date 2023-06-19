using UnityEngine;

public class ActivatedTrackpoint : TrackPoint
{
    [SerializeField] private GameObject hint;

    private void Awake()
    {
        hint.SetActive(IsTarget);
    }

    protected override void OnPassed()
    {
        hint.SetActive(false);
    }

    protected override void OnAssignAsTarget()
    {
        hint.SetActive(true);
    }
}