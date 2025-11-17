using ShootEmUp;
using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private Transform _gameWorld;
        [SerializeField] private Transform _canvas;


        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private GameObject _playerPrefab;

        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private Transform _enemiesRoot;
        [SerializeField] private GameObject _enemyPrefab;

        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle().NonLazy();

            Container.Bind<Transform>().WithId(Tags.GAMEWORLD).FromInstance(_gameWorld).AsCached();
            Container.Bind<Transform>().WithId(Tags.CANVAS).FromInstance(_canvas).AsCached();

            Container.BindInterfacesTo<KeyboardInputService>().AsSingle();
            Container.BindInterfacesTo<GameManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<BulletManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<LevelBounds>().FromComponentInHierarchy().AsSingle();

            PlayerBindings();
            EnemyBindings();

            Container.BindInterfacesAndSelfTo<GameBootstrapper>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }

        private void PlayerBindings()
        {
            Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();

            Container.Bind<PlayerFacade>()
                .FromSubContainerResolve()
                .ByNewPrefabInstaller<PlayerInstaller>(_playerPrefab)
                .UnderTransform(_gameWorld)
                .AsSingle()
                .NonLazy();
        }

        private void EnemyBindings()
        {
            Container.Bind<EnemySpawnerConfig>().FromInstance(_enemySpawnerConfig).AsSingle();
            Container.Bind<GameConfig>().FromInstance(_gameConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();
            
            Container.BindFactory<Vector3, Vector3, EnemyFacade, EnemyFacade.Factory>()
                .FromPoolableMemoryPool<Vector3, Vector3, EnemyFacade, EnemyFacadePool>(poolBinder => poolBinder
                    .WithInitialSize(_enemySpawnerConfig.EnemyCount)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<EnemyInstaller>(_enemyPrefab)
                    .UnderTransform(_enemiesRoot));

            Container.BindFactory<Vector3, Vector3, int, BulletType, Bullet, Bullet.Factory>()
                .FromPoolableMemoryPool<Vector3, Vector3, int, BulletType, Bullet, BulletPool>(poolBinder => poolBinder
                    .WithInitialSize(_gameConfig.BulletPoolCount)
                    .FromComponentInNewPrefab(_gameConfig.BulletPrefab)
                    .UnderTransformGroup(Tags.BULLETS));
        }

        class EnemyFacadePool : MonoPoolableMemoryPool<Vector3, Vector3, IMemoryPool, EnemyFacade>
        {
        }

        class BulletPool : MonoPoolableMemoryPool<Vector3, Vector3, int, BulletType, IMemoryPool, Bullet>
        {
        }
    }
}