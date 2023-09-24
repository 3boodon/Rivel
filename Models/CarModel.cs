namespace Rivel.Models {
    internal class CarModel : BaseModel {


        // Reference to Car Brand
        public int CarBrandID { get; set; }  // Foreign key linking to CarBrand model

        // Car Model Details
        public string ModelName { get; set; }

    }
}
