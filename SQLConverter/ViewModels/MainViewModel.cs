using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using ReactiveUI;
using System.Windows.Input;

namespace SQLConverter.ViewModels;

public class MainViewModel : ViewModelBase
{
    private string _beforeSql;
    public string BeforeSql
    {
        get { return _beforeSql; }
        set
        {
            //_beforeSql = value;
            this.RaiseAndSetIfChanged(ref _beforeSql, value);
        }
    }
    private string _afterSql;
    public string AfterSql
    {
        get { return _afterSql; }
        set
        {
            //_beforeSql = value;
            this.RaiseAndSetIfChanged(ref _afterSql, value);
        }
    }
    //public string BeforeSql { get => _beforeSql; set => this.RaiseAndSetIfChanged(ref _beforeSql, value); }
    public ICommand ConvertCommand { get; }
    public ICommand CopyCommand { get; }
    public ICommand PasteCommand { get; }
    public MainViewModel()
    {
        ConvertCommand = ReactiveCommand.Create(async () => 
        {
            AfterSql = BeforeSql;
          
        });

        CopyCommand = ReactiveCommand.Create(async () =>
        {
            var window = ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).Windows[0];
            var clipboard = window.Clipboard;
            var dataObject = new DataObject();
            dataObject.Set(DataFormats.Text, AfterSql);
            await clipboard.SetDataObjectAsync(dataObject);
        });
        PasteCommand = ReactiveCommand.Create(async () =>
        {
            var window = ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime).Windows[0];
            var clipboard = window.Clipboard;
            BeforeSql = await clipboard.GetTextAsync();

        });
    }
}
