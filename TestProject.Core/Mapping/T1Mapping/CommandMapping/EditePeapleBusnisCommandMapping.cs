using TestProject.Core.Fetures.PeapleBusnises.Commads.Models;
using TestProject.Data.Entity;

namespace TestProject.Core.Mapping.T1Mapping
{
    public partial class T1Profile
    {
        public void EditePeapleBusnisCommandMapping()
        {
            CreateMap<UpdatePeapleBusniseCommand, PeapleBusnise>();

        }
    }
}
