using Zenject;

public class TestSceneInstaller : MonoInstaller{
	public override void InstallBindings(){
		Container.BindFactory<TestObject, TestFactory>().FromNewComponentOnNewGameObject();
	}
}
