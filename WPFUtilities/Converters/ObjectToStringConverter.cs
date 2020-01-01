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
    public class ObjectToStringConverter : SmartConverterTemplate<object, string>
    {
        public override string Convert(object value)
        {
            return value.ToString();
        }
    }
}
