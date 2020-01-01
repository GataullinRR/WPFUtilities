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
    public class DoubleRoundConvertor : SmartConverterTemplate<double, double, int>
    {
        public override double Convert(double value, int numOfDigits)
        {
            return value.Round(numOfDigits);
        }

        public override double ConvertBack(double value, int parameter)
        {
            return value;
        }
    }
}
