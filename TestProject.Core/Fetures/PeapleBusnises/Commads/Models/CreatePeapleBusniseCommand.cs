using MediatR;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.PeapleBusnises.Commads.Models
{
    public class CreatePeapleBusniseCommand : IRequest<Response<string>>
    {
        public string Name_user { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string Name_Shope { get; set; }
        public string region_shope { get; set; }
    }
}
