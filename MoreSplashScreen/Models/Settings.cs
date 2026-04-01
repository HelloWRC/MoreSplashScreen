using CommunityToolkit.Mvvm.ComponentModel;

namespace MoreSplashScreen.Models;

public partial class Settings : ObservableObject
{
    /// <summary>
    /// 启动动画类型
    /// </summary>
    /// <value>
    /// 0 - 应用默认 <br/>
    /// 1 - Android Studio <br/>
    /// 2 - Microsoft 365 <br/>
    /// 3 - Microsoft Office (2013-2021)<br/>
    /// 4 - JetBrains IDEs <br/>
    /// 5 - dotUltimate <br/>
    /// 6 - IslandOnTop <br/>
    /// 7 - Adobe Photoshop <br/>
    /// </value>
    [ObservableProperty] private int _splashKind = 0;

    [ObservableProperty] private bool _isCustomAndroidStudioSplashImageEnabled = false;

    [ObservableProperty] private string _customAndroidStudioSplashImagePath = "";

    [ObservableProperty] private int _customAndroidStudioSplashImageKind = 0;
}
