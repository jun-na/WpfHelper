using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using R3;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace WpfHelper.ViewModels;
/// <summary>
/// ReactivePropertyのViewModelのベースとなるクラス
/// </summary>
public class ReactiveViewModelBase : INotifyPropertyChanged, IDisposable
{

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public ReactiveViewModelBase()
    {
        AddToDisposable();
    }

#pragma warning disable CS0067 // イベント 'SearchViewModel.PropertyChanged' は使用されていません
    public event PropertyChangedEventHandler? PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    private CompositeDisposable disposable = new CompositeDisposable();
    // 一括でReactivePropertyのプロパティをdisposableに追加
    private void AddToDisposable()
    {
        var reactiveProperties = GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Where(p => p.PropertyType.IsGenericType &&
                    (p.PropertyType.GetGenericTypeDefinition() == typeof(BindableReactiveProperty<>) ||
                     p.PropertyType.GetGenericTypeDefinition() == typeof(ReactiveCommand<Unit>)));

        // 各ReactiveプロパティをCompositeDisposableに追加
        foreach (var property in reactiveProperties)
        {
            var reactiveObject = property.GetValue(this);
            if (reactiveObject is IDisposable ro)
            {
                ro.AddTo(disposable);
            }
        }
    }
    public void Dispose()
    {
        BeforeDispose();
        disposable.Dispose();
    }


    protected virtual void BeforeDispose() { }
}

