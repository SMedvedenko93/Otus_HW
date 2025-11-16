using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace ShootEmUpZenject
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private Transform _gameWorld;
        [SerializeField] private Transform _canvas;


        [FormerlySerializedAs("_config")][SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private Character _characterPrefab;

        [SerializeField] private EnemySpawnerConfig _enemySpawnerConfig;
        [SerializeField] private Transform _enemiesRoot;
        [SerializeField] private EnemyView _enemyPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId("GameWorld").FromInstance(_gameWorld).AsCached();
            Container.Bind<Transform>().WithId("Canvas").FromInstance(_canvas).AsCached();

            Container.BindInterfacesTo<KeyboardInputService>().AsSingle();

            CharacterBindings();
            EnemyBindings();

            Container.BindInterfacesAndSelfTo<GameBootstrapper>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }

        private void CharacterBindings()
        {
            Container.Bind<CharacterConfig>().FromInstance(_characterConfig).AsSingle();
            Character character = Container.InstantiatePrefabForComponent<Character>(_characterPrefab, _characterConfig.SpawnPosition, Quaternion.identity, _gameWorld);
            Rigidbody2D rigidbody2D = character.GetComponent<Rigidbody2D>();
            Container.BindInterfacesAndSelfTo<Character>().FromInstance(character);

            Container.Bind<IMovable>().To<CharacterMovement>().AsSingle().WithArguments(rigidbody2D);
            Container.BindInterfacesAndSelfTo<BulletSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AttackSystem>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<CharacterSystem>().AsSingle().NonLazy();

        }

        private void EnemyBindings()
        {
            Container.Bind<EnemySpawnerConfig>().FromInstance(_enemySpawnerConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();

            Container.BindMemoryPool<EnemyView, Enemy.Pool>()
                .WithFixedSize(_enemySpawnerConfig.EnemyCount)
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransform(_enemiesRoot);
        }
    }
}