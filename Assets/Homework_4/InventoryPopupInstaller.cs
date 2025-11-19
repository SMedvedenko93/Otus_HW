using Zenject;

namespace MVx
{
    public class InventoryPopupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPopupView>().To<InventoryPopupView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<InventoryPopupPresenter>().AsSingle().NonLazy();
        }
    }
}