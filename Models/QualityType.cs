using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace cs_dotnet_maui
{
    public enum QualityType
    {
        Unique,
        Vintage,
        Genuine,
        Strange,
        Unusual,
        Community,
    }

    public static class QualityColors
    {
        public static readonly Color Unique = Color.FromArgb("#FFD700");
        public static readonly Color Vintage = Color.FromArgb("#476291");
        public static readonly Color Genuine = Color.FromArgb("#4d7455");
        public static readonly Color Strange = Color.FromArgb("#CF6A32");
        public static readonly Color Unusual = Color.FromArgb("#8650AC");
        public static readonly Color Community = Color.FromArgb("#70B04A");

        public static Color FromType(QualityType type)
        {
            return type switch
            {
                QualityType.Unique => Unique,
                QualityType.Vintage => Vintage,
                QualityType.Genuine => Genuine,
                QualityType.Strange => Strange,
                QualityType.Unusual => Unusual,
                QualityType.Community => Community,
                _ => Colors.White, // Default to white if not implemented
            };
        }
    }
}
