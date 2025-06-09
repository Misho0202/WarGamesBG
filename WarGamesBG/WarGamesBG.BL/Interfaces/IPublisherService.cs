using WarGamesBG.Models.DTO;
using WarGamesBG.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WarGamesBG.BL.Interfaces
{
    public interface IPublisherService
    {
        Task<List<GetAllGamesByPublisherResponse>> GetAllGameDetails();

    }
}

