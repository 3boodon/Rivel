
namespace Rivel.Models
{
    public class CarStatus : Timestamps
    {

        public CarStatus(int statusId, string statusName)
        {
            CarStatusId = statusId;
            CarStatusName = statusName;
        }
        public CarStatus(string statusName)
        {
            CarStatusName = statusName;
        }
        public int CarStatusId { get; set; }
        public string CarStatusName { get; set; }
    }
}
