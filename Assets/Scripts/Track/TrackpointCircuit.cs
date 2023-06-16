using UnityEngine;
using UnityEngine.Events;

public enum TrackType
{
    Circular,
    Sprint
}
public class TrackpointCircuit : MonoBehaviour
{
    //events
    public event UnityAction<TrackPoint> TrackpointTriggered;
    public event UnityAction<int> LapCompleted;
    //prefs
    [SerializeField] private TrackType trackType;
    public TrackType TrackType => trackType;

    [SerializeField] private TrackPoint[] points;

    private int lapsCompleted = -1;

    private void Awake()
    {
        BuildCircuit();
    }
    private void OnEnable()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].Triggered += OnTrackPointTriggered;
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].Triggered -= OnTrackPointTriggered;
        }
    }
    private void Start()
    {
        points[0].AssignAsTarget();
    }
    #region Private API
    [ContextMenu(nameof(BuildCircuit))]
    private void BuildCircuit()
    {
        points = new TrackPoint[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i).GetComponent<TrackPoint>();

            if (points[i] == null)
            {
                Debug.LogError("No trackpoint");
                return;
            }
            points[i].Reset();
        }
        for (int i = 0; i < points.Length - 1; i++)
        {
            points[i].next = points[i + 1];
        }
        if( trackType == TrackType.Circular)
        {
            points[points.Length - 1].next = points[0];
        }
        points[0].isFirst = true;

        if (trackType == TrackType.Sprint) points[points.Length - 1].isLast = true;
        if (trackType == TrackType.Circular) points[0].isLast = true;
    }
    private void OnTrackPointTriggered(TrackPoint point)
    {
        if (!point.IsTarget) return;

        point.Passed();
        point.next?.AssignAsTarget();

        if (point.isLast)
        {
            lapsCompleted++;

            if (trackType == TrackType.Sprint) LapCompleted?.Invoke(lapsCompleted);
            else
            {
                if (lapsCompleted > 0)
                {
                    LapCompleted?.Invoke(lapsCompleted);
                }
            }
        }
    }
    #endregion
}
