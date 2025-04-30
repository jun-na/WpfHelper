using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfHelper.Converters;

[ValueConversion(typeof(Enum), typeof(bool))]
public class RadioButtonConverter : IValueConverter
{
    // Enumからboolへの変換
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null)
        {
            return false;
        }
        return value.ToString() == parameter.ToString();
    }

    // boolからEnumへの変換
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || parameter == null) return Binding.DoNothing;
        if ((bool)value && parameter != null)
        {
            var stringValue = parameter.ToString();
            if (stringValue == null)
            {
                throw new ArgumentException("文字列がまちがっています。");
            }
            return Enum.Parse(targetType, stringValue);
        }
        return Binding.DoNothing;
    }
}
