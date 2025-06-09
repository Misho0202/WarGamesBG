using WarGamesBG.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarGamesBG.DL.Cache
{
    public interface ICacheRepository<TKey, TData> where TData : ICacheItem<TKey>
    {
        Task<IEnumerable<TData>> FullLoad();

        Task<IEnumerable<TData>> DifLoad(DateTime lastExecuted);
    }
}
