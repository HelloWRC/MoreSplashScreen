using ClassIsland.Core.Abstractions.Controls;
using ClassIsland.Core.Attributes;

namespace MoreSplashScreen.Views.SettingsPages;

/// <summary>
/// SplashSettingsPage.xaml 的交互逻辑
/// </summary>
[SettingsPageInfo("dev.hellowrc.classisland.noMoreSplash.splashSettings", "更多启动屏幕", "\ueea1", "\ueea0")]
public partial class SplashSettingsPage : SettingsPageBase
{
    public Plugin Plugin { get; }

    public static readonly Dictionary<string, string> InternalImages = new()
    {
        {"Griseo", "avares://MoreSplashScreen/Assets/AndroidStudio/Griseo.webp"},
        {"RyouYamada", "avares://MoreSplashScreen/Assets/AndroidStudio/RyouYamada.webp"},
        {"Misha", "avares://MoreSplashScreen/Assets/AndroidStudio/Misha.webp"},
    };

    public SplashSettingsPage(Plugin plugin)
    {
        Plugin = plugin;
        InitializeComponent();
    }
}