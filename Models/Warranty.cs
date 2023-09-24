namespace Rivel.Models {
    internal class Warranty : BaseModel {


        // Warranty Details
        public string WarrantyName { get; set; }  // e.g., "Basic Coverage", "Full Coverage"
        public string Description { get; set; }  // Detailed description of what the warranty covers
        public decimal Cost { get; set; }  // Cost of the warranty per day

        // Coverage Details
        public bool CoversDamage { get; set; }  // Does it cover accidental damage?
        public bool CoversTheft { get; set; }  // Does it cover theft?
        public bool CoversBreakdown { get; set; }  // Does it cover breakdowns?

        // Limitations and Exclusions
        public string Limitations { get; set; }  // e.g., "Not valid outside the country"
        public string Exclusions { get; set; }  // e.g., "Does not cover tire punctures"

    }
}
