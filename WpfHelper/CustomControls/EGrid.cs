using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfHelper.CustomControls;
/// <summary>
/// グリッドのカスタムコントロール
/// </summary>
public class EGrid : Grid
{


    private static readonly DependencyProperty ColumnsProperty =
        DependencyProperty.Register("Columns",
            typeof(string),
            typeof(EGrid),
            new PropertyMetadata(string.Empty, OnColumnsChanged));

    /// <summary>
    /// カラム定義プロパティ
    /// </summary>
    public string Columns
    {
        get
        {
            return (string)GetValue(ColumnsProperty);
        }
        set
        {
            SetValue(ColumnsProperty, value);
        }
    }

    private static readonly DependencyProperty RowsProperty =
        DependencyProperty.Register(
            "Rows",
            typeof(string),
            typeof(EGrid),
            new PropertyMetadata(string.Empty, OnRowsChanged));

    /// <summary>
    /// ロウ定義プロパティ
    /// </summary>
    public string Rows
    {
        get
        {
            return (string)GetValue(RowsProperty);
        }
        set
        {
            SetValue(RowsProperty, value);
        }
    }

    public enum FlowDirection
    {
        // なし
        None = 0,
        // 左から右
        Horizontal = 1,
        // 上から下
        Vertical = 2,
    }
    private static readonly DependencyProperty FlowDirectionProperty =
        DependencyProperty.Register(
            "FlowDirection",
            typeof(FlowDirection),
            typeof(EGrid),
            new PropertyMetadata(FlowDirection.None));

    public FlowDirection Direction
        {
        get
        {
            return (FlowDirection)GetValue(FlowDirectionProperty);
        }
        set
        {
            SetValue(FlowDirectionProperty, value);
        }
    }


    /// <summary>
    /// Columns変更時の処理
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private static void OnColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Grid取得
        var grid = d as EGrid;
        if (grid == null)
        {
            return;
        }

        // カラム定義取得
        var strArray = d.GetValue(ColumnsProperty) as string;
        if (strArray == null)
        {
            return;
        }

        // 文字列からGridLengthへのコンバータ
        var converter = new GridLengthConverter();

        // カラム定義をGridに設定
        grid.ColumnDefinitions.Clear();
        foreach (var item in strArray.Split(","))
        {
            // 文字列をGridLengthに変換
#pragma warning disable CS8605 // null の可能性がある値をボックス化解除しています。
            grid.ColumnDefinitions.Add(value: new ColumnDefinition() { Width = (GridLength)converter.ConvertFromString(item) });
#pragma warning restore CS8605 // null の可能性がある値をボックス化解除しています。
        }

        // レイアウトを更新
        grid.UpdateLayout();
    }


    /// <summary>
    /// Rows変更時の処理
    /// </summary>
    /// <param name="d"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private static void OnRowsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        // Grid取得
        var grid = d as EGrid;
        if (grid == null)
        {
            return;
        }

        // ロウ定義取得
        var strArray = d.GetValue(RowsProperty) as string;
        if (strArray == null)
        {
            return;
        }

        // カラム定義をGridに設定
        GridLengthConverter converter = new GridLengthConverter();

        // ロウ定義をGridに設定
        grid.RowDefinitions.Clear();
        foreach (var item in strArray.Split(","))
        {
            // 文字列をGridLengthに変換
#pragma warning disable CS8605 // null の可能性がある値をボックス化解除しています。
            grid.RowDefinitions.Add(new RowDefinition() { Height = (GridLength)converter.ConvertFromString(item) });
#pragma warning restore CS8605 // null の可能性がある値をボックス化解除しています。
        }

        // レイアウトを更新
        grid.UpdateLayout();
    }


    /// <summary>
    /// レイアウトのカスタム
    /// </summary>
    /// <param name="constraint">Constraint</param>
    /// <returns>Desired size</returns>
    protected override Size MeasureOverride(Size constraint)
    {
        int columnCount = ColumnDefinitions.Count;
        int rowCount = RowDefinitions.Count;
        UIElementCollection internalChildren = base.InternalChildren;
        for (int i = 0; i < Math.Min(InternalChildren.Count, columnCount * rowCount); i++)
        {
            UIElement child = InternalChildren[i];
            if (child != null)
            {
                switch (Direction)
                {
                    case FlowDirection.Horizontal:
                        child.SetValue(ColumnProperty, i % columnCount);
                        child.SetValue(RowProperty, i / columnCount);
                        break;
                    case FlowDirection.Vertical:
                        child.SetValue(ColumnProperty, i / rowCount);
                        child.SetValue(RowProperty, i % rowCount);
                        break;
                    default:
                        // なしの場合はデフォルトの動作を行う
                        break;
                }
            }
        }
        return base.MeasureOverride(constraint);
    }
}
