using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.ControlTables.Commands.Models
{
    public class AddDynamicTableCommand : IRequest<Response<string>>
    {
        public string TableName { get; set; }
        public string Item { get; set; }
        public string Discription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int CountImage { get; set; }
        public string NameImage { get; set; }
    }
}
