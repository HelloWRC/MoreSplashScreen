using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Abstractions.Views;

namespace MoreSplashScreen.Views.SplashScreens;

public partial class AllsSplashScreen : SplashWindowBase
{
    public ISplashService SplashService { get; }
    
    public AllsSplashScreen(ISplashService splashService)
    {
        SplashService = splashService;
        InitializeComponent();
    }
}