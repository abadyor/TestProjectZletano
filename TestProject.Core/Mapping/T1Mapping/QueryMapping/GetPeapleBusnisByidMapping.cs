using TestProject.Core.Fetures.PeapleBusnises.Querys.Models;
using TestProject.Data.Entity;

namespace TestProject.Core.Mapping.T1Mapping
{
    public partial class T1Profile
    {
        public void GetPeapleBusnisByidMapping()
        {
            CreateMap<PeapleBusnise, GetPeapleBusniseByIdQuery>();
        }
    }
}

