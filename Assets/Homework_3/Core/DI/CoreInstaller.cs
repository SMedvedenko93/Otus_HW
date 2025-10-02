using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    //[SerializeField] private UIConfig _config;
    [SerializeField] private Transform _gameWorld;
    [SerializeField] private Transform _canvas;
    [SerializeField] private Transform _audioSystem;

    //[SerializeField] private CurrencyView _currencyView;
    //[SerializeField] private GemsView _gemsView;

    //[SerializeField] private AudioConfig _audioConfig;
    //[SerializeField] private List<MobConfig> _mobConfigs;

    public override void InstallBindings()
    {
		/*
        Container.Bind<EventBus>().AsSingle().NonLazy();

        Container.Bind<Transform>().WithId("GameWorld").FromInstance(_gameWorld).AsCached();
        Container.Bind<Transform>().WithId("Canvas").FromInstance(_canvas).AsCached();
        Container.Bind<Transform>().WithId("AudioSystem").FromInstance(_audioSystem).AsCached();

        Container.Bind<UIConfig>().FromInstance(_config).AsSingle();
        Container.Bind<AudioConfig>().FromInstance(_audioConfig).AsSingle();

        Container.Bind<CryptoService>().AsSingle();

        Container.BindInterfacesAndSelfTo<SaveSystem>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<AudioSystem>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<LocationSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<MobSystem>().AsSingle();

        Container.BindInterfacesAndSelfTo<QuestSystem>().AsSingle().NonLazy();
        Container.Bind<IQuestView>().To<QuestView>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<QuestPresenter>().AsSingle().NonLazy();

        Container.BindInterfacesTo<ScreenManager>().AsSingle().NonLazy();
        Container.BindInterfacesTo<PopupManager>().AsSingle().NonLazy();

        Container.BindInterfacesAndSelfTo<AdsSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<AnalyticsSystem>().AsSingle();

        Container.Bind<UISystem>().AsSingle();

        Container.BindInterfacesAndSelfTo<Catscene>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameBootstrapper>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
		*/
    }

}