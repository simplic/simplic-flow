using System.Collections.Generic;

namespace Simplic.Flow.Editor.UI
{
    public static class Constants
    {
        public const string FlowStrokeColor = "#FEFEFE";
        public const string FlowHighlightColor = "White";       
        
        public readonly static IDictionary<string, string> StrokeColors = new Dictionary<string, string> {
                { "Object", "#00A6F2" },

                { "Boolean", "#920000" },

                { "Byte", "#E679A7" },
                { "SByte", "#E679A7" },

                { "Int16", "#22DFAB" },
                { "UInt16", "#22DFAB" },
                { "Int32", "#22DFAB" },
                { "UInt32", "#22DFAB" },
                { "Int64", "#22DFAB" },
                { "UInt64", "#22DFAB" },

                { "Single", "#9DFC44" },
                { "Double", "#9DFC44" },
                { "Decimal", "#9DFC44" },

                { "String", "#FE00D2" },

                { "ValueType", "#FEC827" },
                { "ClassType", "#FD7100" }
        };

        public readonly static IDictionary<string, string> HighlightColors = new Dictionary<string, string> {
                { "Object", "#00B2FF" },

                { "Boolean", "#AA0000" },

                { "Byte", "#FF87BB" },
                { "SByte", "#FF87BB" },

                { "Int16", "#27F7BC" },
                { "UInt16", "#27F7BC" },
                { "Int32", "#27F7BC" },
                { "UInt32", "#27F7BC" },
                { "Int64", "#27F7BC" },
                { "UInt64", "#27F7BC" },

                { "Single", "#AEFF5E" },
                { "Double", "#AEFF5E" },
                { "Decimal", "#AEFF5E" },

                { "String", "#FF3AD4" },

                { "ValueType", "#FCB900" },
                { "ClassType", "#FC8A32" }
        };
    }
}
