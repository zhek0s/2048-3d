using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CubeSpawner cubeSpawner;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private InputController inputController;

    [SerializeField] private float mergeThreshold = 2f;
    [SerializeField] private float bounceForce = 4f;
    public override void InstallBindings()
    {
        Container.Bind<CubeSpawner>().FromInstance(cubeSpawner).AsSingle();
        Container.Bind<ScoreManager>().FromInstance(scoreManager).AsSingle();
        Container.Bind<InputController>().FromInstance(inputController).AsSingle();

        Container.Bind<float>().WithId("MergeThreshold").FromInstance(mergeThreshold);
        Container.Bind<float>().WithId("MergeBounceForce").FromInstance(bounceForce);

        Container.Bind<MergeService>().AsSingle();
        Container.Bind<GameStateService>().AsSingle();
        Container.Bind<GameOverService>().AsSingle();
        Container.Bind<AutoMergeBoosterService>().AsSingle();

        Container.Bind<KillZone>().FromComponentInHierarchy().AsSingle();
        Container.Bind<GameController>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CubePool>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ParticlePool>().FromComponentInHierarchy().AsSingle();
    }
}