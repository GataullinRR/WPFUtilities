using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Utilities;
using Utilities.Extensions;

namespace WPFUtilities
{
    public static class WPFCommonUtils
    {
        public static SolidColorBrush CreateBrush(string argb)
        {
            var separated = argb.ToUpper().ToList()
                .SubArrays(2)
                .Select(arr => arr.Aggregate()).ToList();

            return new SolidColorBrush(Color.FromArgb(
                Convert.ToByte(separated[0], 16),
                Convert.ToByte(separated[1], 16),
                Convert.ToByte(separated[2], 16),
                Convert.ToByte(separated[3], 16)
                ));
        }
    }
}
