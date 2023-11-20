using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using SQLConverter.ViewModels;
using SQLConverter.Views;
using System.Diagnostics;
using System.Reactive;
using System;
using Live.Avalonia;
using ReactiveUI;

namespace SQLConverter;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel() 
            };
        }
        //else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        //{
        //    singleViewPlatform.MainView = new MainView
        //    {
        //        DataContext = MainWindow
        //    };
        //}
        base.OnFrameworkInitializationCompleted();
    }

    // When any of the source files change, a new version of the assembly is 
    // built, and this method gets called. The returned content gets embedded 
    // into the LiveViewHost window.
    public object CreateView(Window window)
    {
        if (window.DataContext == null)
            window.DataContext = new MainViewModel();

        // The AppView class will inherit the DataContext
        // of the window. The AppView class can be a 
        // UserControl, a Grid, a TextBlock, whatever.
        return new MainWindow();
    }

    private static bool IsProduction()
    {
#if DEBUG
        return false;
#else
        return true;
#endif
    }
}
