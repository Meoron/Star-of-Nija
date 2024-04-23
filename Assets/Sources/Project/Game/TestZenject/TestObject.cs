using Sources.Project.Managers;
using UnityEngine;
using Zenject;

public class TestObject : MonoBehaviour
{
    [Inject]
    public void Init(IWindowManager windowManager){
        Debug.Log(windowManager);
    }
}
