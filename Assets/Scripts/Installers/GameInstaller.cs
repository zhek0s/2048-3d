using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private InputController inputController;

    [SerializeField] private float mergeThreshold = 2f;
    public override void InstallBindings()
    {
        Container.Bind<CubeSpawner>().FromInstance(cubeSpawner).AsSingle();
        Container.Bind<ScoreManager>().FromInstance(scoreManager).AsSingle();
        Container.Bind<InputController>().FromInstance(inputController).AsSingle();

        Container.Bind<float>().WithId("MergeThreshold").FromInstance(mergeThreshold);

        Container.Bind<MergeService>().AsSingle().NonLazy();
    }
}