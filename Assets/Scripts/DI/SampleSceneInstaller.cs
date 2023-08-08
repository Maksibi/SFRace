using UnityEngine;
using Zenject;

public class SampleSceneInstaller : MonoInstaller
{
    [SerializeField] private Car car;
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private CarInputControl control;
    [SerializeField] private RaceInputController raceController;
    [SerializeField] private RaceResultTime resultTime; 

    public override void InstallBindings()
    {
        Bind(car);
        Bind(trackpointCircuit);
        Bind(raceStateTracker);
        Bind(raceTimeTracker);
        Bind(control);
        Bind(raceController);
        Bind(resultTime);
    }
    private void Bind<T>(T instance)
    {
        Container.Bind<T>()
                .FromInstance(instance)
                .AsSingle()
                .NonLazy();
    }
}