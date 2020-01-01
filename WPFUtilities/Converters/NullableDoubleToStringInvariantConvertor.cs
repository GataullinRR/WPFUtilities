using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.Types;
using Utilities.Extensions;

namespace WPFUtilities.Converters
{
    public class NullableDoubleToStringInvariantConvertor : SmartConverterTemplate<double?, string, int?>
    {
        public override string Convert(double? value, int? numOfDigits)
        {
            return value.HasValue
                ? value.Value.Round(numOfDigits ?? 15).ToStringInvariant()
                : "-";
        }

        public override double? ConvertBack(string value, int? parameter)
        {
            return value == "-"
                ? (double?)null
                : value.ParseToDoubleInvariant();
        }
    }
}
