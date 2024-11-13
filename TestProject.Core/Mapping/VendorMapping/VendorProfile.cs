using AutoMapper;

namespace TestProject.Core.Mapping.VendorMapping
{
    public partial class VendorProfile : Profile
    {
        public VendorProfile()
        {
            GetVendorListMapping();
            GetVendorByIdMapping();
            AddVendorCommandMapping();
        }
    }
}
