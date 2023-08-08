using UnityEngine;

public interface IDependency<T>
{
    void Construct(T obj);
}
public class SceneDependencies : MonoBehaviour
{
    #region Prefs
    [SerializeField] private Car car;
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private CarInputControl control;
    [SerializeField] RaceInputController raceController;
    #endregion
    private void Bind(MonoBehaviour mono)
    {
        if (mono is IDependency<TrackpointCircuit>) (mono as IDependency<TrackpointCircuit>)?.Construct(trackpointCircuit);
        if (mono is IDependency<RaceStateTracker>) (mono as IDependency<RaceStateTracker>)?.Construct(raceStateTracker);
        if (mono is IDependency<RaceTimeTracker>) (mono as IDependency<RaceTimeTracker>)?.Construct(raceTimeTracker);
        if (mono is IDependency<CarInputControl>) (mono as IDependency<CarInputControl>)?.Construct(control);
        if (mono is IDependency<Car>) (mono as IDependency<Car>)?.Construct(car);
        if (mono is IDependency<RaceInputController>) (mono as IDependency<RaceInputController>)?.Construct(raceController);
    }
    private void Awake()
    {
        MonoBehaviour[] monoBehaviours = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < monoBehaviours.Length; i++)
        {
            Bind(monoBehaviours[i]);
        }
    }
}
