using Sources.Common.Input;
using Sources.Project.Game.Constants;
using UnityEngine;
using Zenject;

public class TestSpawner : MonoBehaviour{
    private IInputService _inputManage;
    private TestFactory _factory;
    
    [Inject]
    private void InjectingServices(IInputService inputService){
        _inputManage = inputService;
    }

    [Inject]
    private void InjectingSceneComponents(TestFactory factory){
        _factory = factory;
    }

    private void Update()
    {
        if (_inputManage.GetButtonDown(InputConstants.TestKey)){
            Spawn();
        }
    }

    private void Spawn(){
        _factory.Create();
    }
}
