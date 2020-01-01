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
    public class IntToDoubleConverter : SmartConverterTemplate<int, double>
    {
        public override double Convert(int value)
        {
            return value;
        }

        public override int ConvertBack(double value)
        {
            return value.Round();
        }
    }
}
