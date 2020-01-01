using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUtilities.Converters
{
    public class NullToFalseConverter : ConverterTemplate<object, bool>
    {
        public override bool Convert(object value)
        {
            return value != null;
        }
    }
}
