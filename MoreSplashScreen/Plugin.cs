
using System.IO;
using ClassIsland.Core;
using ClassIsland.Core.Abstractions;
using ClassIsland.Core.Abstractions.Views;
using ClassIsland.Core.Attributes;
using ClassIsland.Core.Controls.CommonDialog;
using ClassIsland.Core.Extensions.Registry;
using ClassIsland.Shared.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoreSplashScreen.Models;
using MoreSplashScreen.Views.SettingsPages;
using MoreSplashScreen.Views.SplashScreens;

namespace MoreSplashScreen;

[PluginEntrance]
public class Plugin : PluginBase
{
    public Settings Settings { get; set; } = new();

    public override void Initialize(HostBuilderContext context, IServiceCollection services)
    {
        Settings = ConfigureFileHelper.LoadConfig<Settings>(Path.Combine(PluginConfigFolder, "Settings.json"));  // 加载配置文件
        Settings.PropertyChanged += (sender, args) =>
        {
            ConfigureFileHelper.SaveConfig<Settings>(Path.Combine(PluginConfigFolder, "Settings.json"), Settings);  // 保存配置文件
        };

        services.AddSettingsPage<SplashSettingsPage>();
        switch (Settings.SplashKind)
        {
            case 1:  // Android Studio
                services.AddTransient<SplashWindowBase, AndroidStudioSplashScreen>();
                break;
            case 2:  // Microsoft 365
                services.AddTransient<SplashWindowBase, Microsoft365SplashScreen>();
                break;
            case 3:  // Office old
                services.AddTransient<SplashWindowBase, OfficeLegacySplashScreen>();
                break;
            case 6:  // IslandOnTop
                services.AddTransient<SplashWindowBase, IslandSplashScreen>();
                break;
            case 7:  // Adobe Photoshop
                services.AddTransient<SplashWindowBase, AdobePhotoshopSplashScreen>();
                break;
        }
    }   
}
