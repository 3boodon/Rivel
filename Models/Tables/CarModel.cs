namespace Rivel.Models
{
    public class CarModel : Timestamps
    {
        public CarModel(int modelId, int brandId, string modelName)
        {
            ModelId = modelId;
            BrandId = brandId;
            ModelName = modelName;
        }
        public CarModel(int brandId, string modelName)
        {
            BrandId = brandId;
            ModelName = modelName;
        }
        public int ModelId { get; set; }
        public int BrandId { get; set; }
        public string ModelName { get; set; }
    }
}
