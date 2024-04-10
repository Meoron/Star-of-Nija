#if !DISABLESTEAMWORKS
using Sources.Platform.Steam;
#endif

using Sources.Platforms;
using Sources.Platforms.Windows;
using Zenject;

public class PlatformContextInstallert : MonoInstaller{
	public override void InstallBindings(){
#if UNITY_STANDALONE && !DISABLESTEAMWORKS
		Container.Bind<IPlatformContext>().To<SteamContext>().AsSingle().NonLazy();
#else
		Container.Bind<IPlatformContext>().To<WindowsContext>().AsSingle().NonLazy();
#endif
	}
}
