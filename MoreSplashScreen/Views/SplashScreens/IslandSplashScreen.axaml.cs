using System.ComponentModel;
using Avalonia;
using Avalonia.Animation.Easings;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Rendering.Composition;
using ClassIsland.Core.Abstractions.Services;
using ClassIsland.Core.Abstractions.Views;
using ReactiveUI;

namespace MoreSplashScreen.Views.SplashScreens;

/// <summary>
/// IslandSplashScreen.xaml 的交互逻辑
/// </summary>
public partial class IslandSplashScreen : SplashWindowBase
{
    public ISplashService SplashService { get; }
    public Plugin Plugin { get; }

    private double _lastProgress = 0.0;
    private double _lastProgressDelta = 0.0;
    private IDisposable? _splashStatusObserver;

    private bool _canClose = false;

    public Image? SplashImage { get; }

    public IslandSplashScreen(ISplashService splashService, Plugin plugin)
    {
        SplashService = splashService;
        Plugin = plugin;
        InitializeComponent();
        Loaded += OnLoaded;
    }

    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        var visualProgressBarFill = ElementComposition.GetElementVisual(ProgressBarFill);
        if (visualProgressBarFill != null)
        {
            visualProgressBarFill.Offset = visualProgressBarFill.Offset with { X = -400 };
            visualProgressBarFill.Opacity = 1.0f;
        }
    }

    void Cal(object sender, RoutedEventArgs e)
    {
        // 获取屏幕的宽度和高度
        var screenWidth = Screens.Primary?.Bounds.Width ?? 0;
        var screenHeight = Screens.Primary?.Bounds.Height ?? 0;

        // 获取窗口的宽度和高度
        var windowWidth = this.Width;
        var windowHeight = this.Height;

        // 计算窗口的左上角位置，使窗口居中

        var x = (int)(screenWidth - windowWidth) / 2;
        //CommonDialog.ShowHint(this.Left.ToString());
        const int y = (int)0;
        Position = new PixelPoint(x, y);
    }

    private void SplashServiceOnProgressChanged(object? sender, double e)
    {
        _ = UpdateAnimationAsync(e);

    }

    private async Task UpdateAnimationAsync(double value, bool isFinal = false)
    {
        var visualProgressBarFill = ElementComposition.GetElementVisual(ProgressBarFill);
        if (visualProgressBarFill == null)
        {
            return;
        }
        
        var currentOffset = visualProgressBarFill.Offset;
        var compositor = visualProgressBarFill.Compositor;
        var progressAnimation = compositor.CreateVector3DKeyFrameAnimation();
        progressAnimation.InsertKeyFrame(1.0f, currentOffset with { X = -400 * (1 - value / 100) }, new CubicEaseOut());
        var duration = isFinal ? TimeSpan.FromSeconds(0.15) : TimeSpan.FromSeconds(Math.Max((value - _lastProgress ) / 8, 0.5));
        progressAnimation.Duration = duration;
        visualProgressBarFill.StartAnimation(nameof(visualProgressBarFill.Offset), progressAnimation);
        _lastProgress = value;

        await Task.Delay(duration);
    }


    private void IslandSplashScreen_OnClosing(object? sender, WindowClosingEventArgs e)
    {
        if (!_canClose)
        {
            e.Cancel = true;
        }
    }

    public override async Task StartSplash()
    {
        SplashService.ProgressChanged += SplashServiceOnProgressChanged;
        _splashStatusObserver ??= SplashService.ObservableForProperty(x => x.SplashStatus)
            .Subscribe(_ => TryRunJobs());
        await base.StartSplash();
    }

    public override async Task EndSplash()
    {
        SplashService.ProgressChanged -= SplashServiceOnProgressChanged;
        _splashStatusObserver?.Dispose();
        _splashStatusObserver = null;
        await UpdateAnimationAsync(100, true);
        _canClose = true;
        await base.EndSplash();
    }
}