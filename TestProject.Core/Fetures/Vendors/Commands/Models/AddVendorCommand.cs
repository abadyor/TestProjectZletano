using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Vendors.Commands.Models
{
    public class AddVendorCommand : IRequest<Response<string>>
    {

        public string GivenNames { get; set; }


        public string Nickname { get; set; }




        public string Gender { get; set; }


        public string DocId { get; set; }


        public string DocType { get; set; }



        public string EmailAddress { get; set; } = string.Empty;


        public string Mobile { get; set; }


        public string Username { get; set; }


        public string Password { get; set; }

        // public DateTime Timestamp_create { get; set; }


        // لاستقبال كود منطقة واحدة
        /*      public List<string> MarketCodes { get; set; } = new List<string>();
              public List<string> RegionCodes { get; set; } = new List<string>();*/
        /*  public List<GetMarketResponse> Markets { get; set; } = new List<GetMarketResponse>();
          public List<GetRegionResponse> Regions { get; set; } = new List<GetRegionResponse>();*/


    }
}
