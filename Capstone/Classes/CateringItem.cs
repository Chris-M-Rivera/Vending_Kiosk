using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class CateringItem
    {
        // This class contains the definition for one catering item

        public string Name { get; set; } 
        public double Price { get; set; }
        public string Type { get; set; }
        public string ProductCode { get; set; }
        public int Quantity { get; set; } = 25;

    }
}
