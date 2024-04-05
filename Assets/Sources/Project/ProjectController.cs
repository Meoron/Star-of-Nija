using Sources.Common;
using Sources.Common.StateMachine;
using Sources.Project.StateMachine;
using UnityEngine;

namespace Sources.Project{
    public interface IProjectController: IController{
        public IProjectContext ProjectContext { get; }
        public IStateMachine StateMachine { get; }
    }
    public class ProjectController : MonoBehaviour, IProjectController{
        private IProjectController _projectControllerImplementation;
        
        public IProjectContext ProjectContext{ get; private set; }
        public IStateMachine StateMachine{ get; private set; }
        private void Awake(){
            ProjectContext = new ProjectContext(this, transform);
            StateMachine = new ProjectSateMachine(this);
            
            DontDestroyOnLoad(gameObject);
            
            StateMachine.ApplyState<InitializationProjectState>();
        }
    }
}
