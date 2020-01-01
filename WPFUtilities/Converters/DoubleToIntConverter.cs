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
    public class DoubleToIntConverter : SmartConverterTemplate<double, int>
    {
        public override int Convert(double value)
        {
            return value.Round();
        }

        public override double ConvertBack(int value)
        {
            return value;
        }
    }
}
