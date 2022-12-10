using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cs_dotnet_maui
{
    public class Item
    {
        public int Id { get; set; }
        public string Name {get; set;}
        public QualityType Quality {get; set;}

        public Color Color
        {
            get
            {
                return QualityColors.FromType(Quality);
            }
        }
    }
}
