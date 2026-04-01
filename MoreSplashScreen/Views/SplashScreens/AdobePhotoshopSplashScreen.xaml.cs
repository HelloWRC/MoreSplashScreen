using System;
using System.Windows;
using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Abstractions.Views;

namespace MoreSplashScreen.Views.SplashScreens;

/// <summary>
/// AdobePhotoshopSplashScreen.xaml 的交互逻辑
/// </summary>
public partial class AdobePhotoshopSplashScreen : SplashWindowBase
{
    public ISplashService SplashService { get; }
    private bool _closed;

    public AdobePhotoshopSplashScreen(ISplashService splashService)
    {
        SplashService = splashService;
        SplashService.SplashEnded += OnSplashEnded;
        InitializeComponent();
    }

    private void OnSplashEnded(object? sender, EventArgs e)
    {
        SplashService.SplashEnded -= OnSplashEnded;
        if (!_closed)
        {
            Dispatcher.InvokeAsync(Close);
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        _closed = true;
        base.OnClosed(e);
    }
}