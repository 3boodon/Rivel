namespace Rivel.Models
{
    public class Warranty : Timestamps
    {
        public Warranty(int warrantyId, string warrantyName)
        {
            WarrantyId = warrantyId;
            WarrantyName = warrantyName;
        }
        public Warranty(string warrantyName)
        {
            WarrantyName = warrantyName;
        }
        public int WarrantyId { get; set; }
        public string WarrantyName { get; set; }
    }

}
