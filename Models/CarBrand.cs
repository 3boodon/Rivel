namespace Rivel.Models {
    internal class CarBrand : BaseModel {

        // Brand Details
        public string BrandName { get; set; }  // e.g., "Toyota", "Ford", "Tesla"
        public string CountryOfOrigin { get; set; }  // e.g., "Japan", "USA"

        // Additional Information
        public string Description { get; set; }  // Brief description of the brand
        public string LogoURL { get; set; }  // URL to the brand's logo image

        // Popular Models (Optional)
        public string PopularModels { get; set; }  // Could be a serialized list or JSON

    }
}
