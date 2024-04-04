#if !DISABLESTEAMWORKS
using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Sources.Platform.Steam {
    public sealed class SteamSaveService : ISaveService {
        public void Write(int userId, int slotId, string key, byte[] data) {
            var dir = Path.Combine(Application.persistentDataPath, userId.ToString(), slotId.ToString());
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }

            var file = Path.Combine(dir, key);
            File.WriteAllBytes(file, data);
        }

        public async Task<byte[]> Read(int userId, int slotId, string key) {
            var dir = Path.Combine(Application.persistentDataPath, userId.ToString(), slotId.ToString());
            var file = Path.Combine(dir, key);
            byte[] result = null;
            try {
                result = Directory.Exists(dir) ? File.ReadAllBytes(file) : null;
            }
            catch (FileNotFoundException e) {
                Debug.LogWarning($"{e.Message} {e.FileName}");
            }

            return result;
        }

        public void Read(int userId, int slotId, string key, Action<byte[]> onCompleted) {
            var dir = Path.Combine(Application.persistentDataPath, userId.ToString(), slotId.ToString());
            var file = Path.Combine(dir, key);
            byte[] result = null;
            try {
                result = Directory.Exists(dir) ? File.ReadAllBytes(file) : null;
            }
            catch (FileNotFoundException e) {
                Debug.LogWarning($"{e.Message} {e.FileName}");
            }
            onCompleted?.Invoke(result);
        }

        public void Delete(int userId, int slotId) {
            var dir = Path.Combine(Application.persistentDataPath, userId.ToString(), slotId.ToString());
            if (Directory.Exists(dir)) {
                Directory.Delete(dir, true);
            }
        }
    }
}
#endif