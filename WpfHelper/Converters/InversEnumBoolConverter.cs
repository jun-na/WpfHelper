using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfHelper.Converters;

/// <summary>
/// Enumの状態によりtrue/falseを返すConverter
/// </summary>
[ValueConversion(typeof(Enum), typeof(bool))]
public class InverseEnumBoolConverter : IValueConverter
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
            return true;
        }
        if (value.ToString() == parameter.ToString())
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// XAMLからViewModelに値を渡すときに呼ばれる
    /// </summary>  
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
