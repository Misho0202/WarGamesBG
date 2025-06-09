using MessagePack;

namespace WarGamesBG.Models.DTO
{
    public interface ICacheItem<T>
    {
        public abstract DateTime DateInserted { get; set; }

        public abstract T GetKey();
    }
}
