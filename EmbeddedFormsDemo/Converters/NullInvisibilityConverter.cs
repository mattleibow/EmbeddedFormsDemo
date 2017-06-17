﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace EmbeddedFormsDemo.Converters
{
	public class NullInvisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value != null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
