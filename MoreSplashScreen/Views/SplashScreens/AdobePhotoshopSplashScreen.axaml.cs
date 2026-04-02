using System;
using System.Windows;
using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Abstractions.Views;
using ReactiveUI;

namespace MoreSplashScreen.Views.SplashScreens;

/// <summary>
/// AdobePhotoshopSplashScreen.xaml 的交互逻辑
/// </summary>
public partial class AdobePhotoshopSplashScreen : SplashWindowBase
{
    private IDisposable? _splashStatusObserver;
    public ISplashService SplashService { get; }

    public AdobePhotoshopSplashScreen(ISplashService splashService)
    {
        SplashService = splashService;
        InitializeComponent();
    }
    
    public override async Task StartSplash()
    {
        _splashStatusObserver ??= SplashService.ObservableForProperty(x => x.SplashStatus)
            .Subscribe(_ => TryRunJobs());
        await base.StartSplash();
    }

    public override async Task EndSplash()
    {
        _splashStatusObserver?.Dispose();
        _splashStatusObserver = null;
        await base.EndSplash();
    }
}