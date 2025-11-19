using UnityEngine;
using Zenject;

namespace MVx
{
    public class CoreInstaller : MonoInstaller
    {
        [SerializeField] private InventoryBase _inventoryBase;
        [SerializeField] private UIConfig _config;
        [SerializeField] private Transform _canvas;
        [SerializeField] private Transform _popupRoot;
        [SerializeField] private InventoryItemView _inventoryItemViewPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId("Canvas").FromInstance(_canvas).AsCached();
            Container.Bind<Transform>().WithId("Popups").FromInstance(_popupRoot).AsCached();
            Container.Bind<UIConfig>().FromInstance(_config).AsSingle();
            Container.Bind<InventoryBase>().FromInstance(_inventoryBase).AsSingle();

            Container.BindInterfacesAndSelfTo<InventorySystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PopupManager>().AsSingle().NonLazy();

            Container.BindFactory<InventoryItem, Transform, InventoryItemView, InventoryItemView.Factory>()
                     .FromComponentInNewPrefab(_inventoryItemViewPrefab);

            Container.Bind<InventoryItemPresenter>().AsTransient();
        }
    }
}