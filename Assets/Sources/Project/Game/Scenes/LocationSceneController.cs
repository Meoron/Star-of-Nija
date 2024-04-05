using System.Threading.Tasks;

namespace Sources.Project.Scenes {
    public sealed class LocationSceneController : SceneController {
        protected override async Task OnInitialize() {
            await base.OnInitialize();
            
            /*var isTutorialActive =
                ProjectContext.QuestManager.GetActiveQuests().FirstOrDefault(x => x.Data.Name == "QDemoTutorial") != null;
            var isTutorialCompleted = ProjectContext.QuestManager.GetCompletedQuests().Contains("QDemoTutorial");
            if (!isTutorialActive && !isTutorialCompleted) {
                ProjectContext.QuestManager.StartQuest("QDemoTutorial");
            }*/
        }
    }
}