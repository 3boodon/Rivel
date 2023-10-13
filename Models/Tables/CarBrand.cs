namespace Rivel.Models
{
    public class Brand : Timestamps
    {
        public Brand(int brandId, string brandName)
        {
            BrandId = brandId;
            BrandName = brandName;
        }
        public Brand(string brandName)
        {
            BrandName = brandName;
        }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }

}
