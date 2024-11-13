using TestProject.Core.Fetures.T11.Queries.Response;
using TestProject.Data.Entity;

namespace TestProject.Core.Mapping.T1Mapping
{

    public partial class T1Profile
    {
        public void GetT1ListMapping()
        {
            CreateMap<T1, GetT1Response>();
        }
    }
}
