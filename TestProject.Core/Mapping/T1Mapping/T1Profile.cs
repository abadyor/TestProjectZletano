using AutoMapper;

namespace TestProject.Core.Mapping.T1Mapping
{
    public partial class T1Profile : Profile
    {
        public T1Profile()
        {
            GetT1ListMapping();
            GetT1SingleMapping();
            AddT1CommandMapping();
            EditeT1CommandMapping();
            DeleteT1CommandMapping();
            GetT1ListTimeMapping();
            GetPeapleBusnisListMapping();
            GetPeapleBusnisByidMapping();
            AddPeapleBusnisCommandMapping();
            EditePeapleBusnisCommandMapping();

        }
    }
}
