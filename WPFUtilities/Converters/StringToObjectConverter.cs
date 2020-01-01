using System;
using System.Collections.Generic;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtilities.Converters
{
    public class StringToObjectConverter : SmartConverterTemplate<string, object>
    {
        public override object Convert(string value)
        {
            return value;
        }

        public override string ConvertBack(object value)
        {
            return (string)value;
        }
    }
}
