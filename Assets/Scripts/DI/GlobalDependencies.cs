using UnityEngine;
using Zenject;

public class GlobalDependencies : MonoInstaller
{
    [SerializeField] private Pause pause;

    private void Awake()
    {
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /*private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
         Bind<Pause>(pause, m) 
    }*/

    private void Bind<T>(T instance)
    {
        Container.Bind<T>()
                .FromInstance(instance)
                .AsSingle()
                .NonLazy();
    }

    public override void InstallBindings()
    {
        Bind(pause);
    }
}
