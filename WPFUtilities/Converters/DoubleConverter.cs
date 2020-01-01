using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Extensions;
using Utilities.Types;

namespace WPFUtilities.Converters
{
    public class DoubleConverter : ConverterTemplate<double, string>
    {
        public override string Convert(double value)
        {
            return value.ToStringInvariant();
        }
        public override double ConvertBack(string value)
        {
            return value.TryParseToDoubleInvariant() ?? 0;
        }
    }
}
