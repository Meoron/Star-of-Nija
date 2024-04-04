using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Sources.Common.AssetManager {
    public sealed class AssetManager {
        private static AssetReference _targetAddressable;
        
        public static async Task<SceneInstance> LoadSceneAsync(string sceneName) {
            return await Addressables.LoadSceneAsync(sceneName).Task;
        }
        
        public static async Task<SceneInstance> UnloadSceneAsync(SceneInstance scene) {
            return await Addressables.UnloadSceneAsync(scene).Task;
        }
        
        public static async Task<T> LoadPrefabAsync<T>(string path) where T : Object {
            var asset = await LoadAsync<T>(path);
            if (asset != null) {
                return asset;
            }
            return null;
        }
        
        public static async Task<Sprite> LoadSpriteAsync(string path) {
            return await LoadAsync<Sprite>(path);
        }
        
        public static async Task<string> LoadConfigAsync(string path) {
            var config = await LoadAsync<TextAsset>(path);
            return config?.text;
        }
        
        public static async Task<AudioClip> LoadAudioAsync(string path) {
            return await LoadAsync<AudioClip>(path);
        }
        
        public static async Task<T> LoadAsync<T>(string path) where T : Object {
            if (string.IsNullOrEmpty(path)) return null;
            
            path = path.ToLowerInvariant();
            path = path.Replace("/", "\\");
            return await Addressables.LoadAssetAsync<T>(path).Task;
        }
    }
}