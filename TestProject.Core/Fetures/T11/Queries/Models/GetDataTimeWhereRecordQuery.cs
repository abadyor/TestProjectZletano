using MediatR;
using TestProject.Data.Entity;

namespace TestProject.Core.Fetures.T11.Queries.Models
{
    public class GetDataTimeWhereRecordQuery : IRequest<(List<T1> records, long elapsedMilliseconds)>
    {
        public int number { get; set; }

    }
}
