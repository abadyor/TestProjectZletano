namespace TestProject.Core.Fetures.ControlTables.Qureries.Response
{
    public class GetImageResponse
    {
        public int Id { get; set; }
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageName { get; set; } // مسار الصورة
    }
}
