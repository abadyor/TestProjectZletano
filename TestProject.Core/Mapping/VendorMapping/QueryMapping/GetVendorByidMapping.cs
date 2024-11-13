using TestProject.Core.Fetures.Vendors.Queries.Response;
using TestProject.Data.Entity;

namespace TestProject.Core.Mapping.VendorMapping
{
    public partial class VendorProfile
    {
        public void GetVendorByIdMapping()
        {
            CreateMap<Vendor, GetVendorByIdResponse>();
        }
    }
}