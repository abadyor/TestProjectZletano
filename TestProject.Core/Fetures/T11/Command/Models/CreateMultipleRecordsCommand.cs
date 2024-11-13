using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.T11.Command.Models
{
    public class CreateMultipleRecordsCommand : IRequest<Response<string>>
    {
        public int NumberOfRecords { get; set; }
    }
}
