using TestProject.Core.Fetures.T11.Command.Models;
using TestProject.Data.Entity;

namespace TestProject.Core.Mapping.T1Mapping
{
    public partial class T1Profile
    {
        public void DeleteT1CommandMapping()
        {
            CreateMap<DeleteT1Command, T1>();

        }
    }
}