namespace TestProject.Core.Fetures.ControlTables.Commands.Models
{
    public class UpdateImageRequestCommand
    {
        public string Item { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageName { get; set; } // المسار الجديد للصورة إذا كان هناك تحديث للصورة
        public string Description { get; set; } // إضافة حقل الوصف
        public string TableName { get; set; }
        public string OldImageName { get; set; } = string.Empty;
    }
}
