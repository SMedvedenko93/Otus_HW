using Zenject;

namespace MVx
{
    public class InventoryItemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //Container.Bind<IInventoryItemView>().To<InventoryItemView>().FromComponentInHierarchy().AsSingle();
            //Container.BindInterfacesAndSelfTo<InventoryItemPresenter>().AsSingle().NonLazy();
        }
    }
}