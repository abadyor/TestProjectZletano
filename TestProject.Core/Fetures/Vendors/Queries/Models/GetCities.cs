﻿using MediatR;
using TestProject.Core.Fetures.Vendors.Queries.Response;
using TestProjectZletano.Core.Base;

namespace TestProject.Core.Fetures.Vendors.Queries.Models
{
    public class GetCities : IRequest<Response<List<GetCitiesResponse>>>
    {
    }
}