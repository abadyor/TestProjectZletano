using TestProject.Core.Fetures.Vendors.Commands.Models;
using TestProject.Data.Entity;

namespace TestProject.Core.Mapping.VendorMapping
{
    public partial class VendorProfile
    {
        public void AddVendorCommandMapping()
        {
            CreateMap<AddVendorCommand, Vendor>();
        }
    }
}
