using UnityEngine;
using Zenject;

namespace ShootEmUpZenject
{
    public class PlayerInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerFacade>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Rigidbody2D>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IMovable>().To<CharacterMovement>().AsSingle();
            Container.Bind<IAttackSystem>().To<PlayerAttackSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerSystem>().AsSingle().NonLazy();
        }
    }
}
