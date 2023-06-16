using UnityEngine;
using UnityEngine.Events;

public class TrackPoint : MonoBehaviour
{
    public event UnityAction<TrackPoint> Triggered;
    protected virtual void OnPassed() { }
    protected virtual void OnAssignAsTarget() { }

    public TrackPoint next;
    //bools
    public bool isFirst, isLast;
    protected bool isTarget;
    public bool IsTarget => isTarget;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Car>() == null) return;

        Triggered?.Invoke(this);
    }
    #region Public API
    public void Passed()
    {
        isTarget = false;
        OnPassed();
    }
    public void AssignAsTarget()
    {
        isTarget = true;
        OnAssignAsTarget();
    }
    public void Reset()
    {
        next = null;
        isFirst = false;
        isLast = false;
    }
    #endregion
}
