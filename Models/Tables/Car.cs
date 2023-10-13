using System;

namespace Rivel.Models
{
    public class Car : Timestamps
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int ColorId { get; set; }
        public int CarStatusId { get; set; }
        public decimal PricePerDay { get; set; }
    }

}
