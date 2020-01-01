using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtilities.Converters
{
    public class BoolLogicMultiConverter : SmartMultiConverterTemplate<bool, bool, string>
    {
        public override bool Convert(bool[] value, string parameter)
        {
            var operation = parameter.ToUpperInvariant();
            switch (operation)
            {
                case "OR":
                    return value.Any(e => e);
                case "AND":
                    return value.All(e => e);

                default:
                    throw new NotSupportedException(operation);
            }
        }
    }
}
