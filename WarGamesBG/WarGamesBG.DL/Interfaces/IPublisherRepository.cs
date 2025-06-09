using WarGamesBG.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WarGamesBG.DL.Interfaces
{
    public interface IPublisherRepository
    {
        Task<Publisher> GetById(string id);

        Task<IEnumerable<Publisher>> GetAllPublishers();

    }
}
