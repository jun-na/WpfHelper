using Kamishibai;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MessageBoxForKamishibai;
public enum BoxType
{
    OK,
    OKCancel,
    YesNo,
}

[OpenDialog]
public class MessageBoxViewModel
{

    public string Title { get; set; } = "メッセージボックス";
    public string Message { get; set; } = "メッセージ";
    public string OkButtonText { get; set; } = "OK";
    public string CancelButtonText { get; set; } = "キャンセル";
    public bool IsOkButtonVisible { get; set; } = true;
    public bool IsCancelButtonVisible { get; set; } = false;
    public bool IsOkButtonEnabled { get; set; } = true;
    public bool IsCancelButtonEnabled { get; set; } = true;

    public ICommand? OkCommand { get; set; }
    public ICommand? CancelCommand { get; set; }

    public MessageBoxViewModel(
        [Inject] IPresentationService presentationService,
        BoxType boxType = BoxType.OK,
        string? title = "info",
        string message = "メッセージ",
        string okButtonText = "OK",
        string cancelButtonText = "キャンセル",
        string yesButtonText = "はい",
        string noButtonText = "いいえ"
    )
    {
        Title = title;
        Message = message;
        OkButtonText = okButtonText;
        CancelButtonText = cancelButtonText;
        switch (boxType)
        {
            case BoxType.OK:
                IsOkButtonVisible = true;
                IsCancelButtonVisible = false;
                break;
            case BoxType.OKCancel:
                IsOkButtonVisible = true;
                IsCancelButtonVisible = true;
                break;
            case BoxType.YesNo:
                OkButtonText = yesButtonText;
                CancelButtonText = noButtonText;
                IsOkButtonVisible = true;
                IsCancelButtonVisible = true;
                break;
            default:
                break;
        }

        OkCommand = new RelayCommand((_) =>
        {
            presentationService.CloseDialogAsync(true);
        });

        CancelCommand = new RelayCommand((_) =>
        {
            presentationService.CloseDialogAsync(false);
        });

    }
}

class RelayCommand : ICommand
{
    private Action<object?> _execute;
    private Func<object?, bool>? _canExecute;
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
    public event EventHandler? CanExecuteChanged = null;
    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }
    public void Execute(object? parameter)
    {
        _execute(parameter);
    }
}
