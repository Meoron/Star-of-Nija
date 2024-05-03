namespace Sources.Project.Managers.UpdateManager{
	public interface ILateUpdatable : IManagedObject{
		public void OnLateUpdate(float deltaTime);
	}
}