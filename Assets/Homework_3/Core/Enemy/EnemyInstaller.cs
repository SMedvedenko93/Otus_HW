using Zenject;

namespace ShootEmUpZenject
{
    public class EnemyInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyFacade>().FromComponentInHierarchy().AsSingle();

            Container.Bind<EnemyView>().FromComponentInHierarchy().AsSingle();

            Container.Bind<Enemy>().AsTransient();
            Container.Bind<IAttackSystem>().To<EnemyAttackSystem>().AsSingle();
        }
    }
}