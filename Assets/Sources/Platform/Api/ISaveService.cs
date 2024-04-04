using System.Threading.Tasks;

namespace Sources.Platform {
    public interface ISaveService {
        void Write(int userId, int slotId, string key, byte[] data);
        Task<byte[]> Read(int userId, int slotId, string key);
        void Delete(int userId, int slotId);
    }
}