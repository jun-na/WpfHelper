using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfHelper.Converters;

/// <summary>
/// EnumをVisibility.Collapseに変換
/// </summary>
[ValueConversion(typeof(Enum), typeof(Visibility))]
public class InverseEnumVisibilityConverter : IValueConverter
{
    /// <summary>
    /// ViewModelからXAMLに値を渡すときに呼ばれる
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="culture"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
        {
            return false;
        }
        if (value.ToString() != parameter.ToString())
        {
            return Visibility.Visible;
        }
        return Visibility.Collapsed;
    }

    /// <summary>
    /// XAMLからViewModelに値を渡すときに呼ばれる
    /// </summary>  
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
