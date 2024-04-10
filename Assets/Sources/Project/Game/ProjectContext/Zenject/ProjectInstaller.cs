using Sources.Common.Input;
using Sources.Platforms;
using Sources.Project.Managers;
using Zenject;

public class ProjectInstaller : MonoInstaller{
    public override void InstallBindings(){
        Container.Bind<IPlatformContext>().To<PlatformContext>().AsSingle().NonLazy();
        Container.Bind<IAccountManager>().To<AccountManager>().AsSingle();
        Container.Bind<IInputManager>().To<InputManager>().FromNew().AsSingle();
        Container.Bind<IWindowManager>().To<WindowManager>().AsSingle();
    }
}
