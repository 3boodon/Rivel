namespace Rivel.Models
{
    public class Color : Timestamps
    {
        public Color(int colorId, string colorName)
        {
            ColorId = colorId;
            ColorName = colorName;
        }
        public Color(string colorName)
        {

            ColorName = colorName;
        }
        public int ColorId { get; set; }
        public string ColorName { get; set; }
    }
}





