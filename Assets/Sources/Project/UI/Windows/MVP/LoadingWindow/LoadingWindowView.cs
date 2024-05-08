using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Project.UI.Windows{
	public class LoadingWindowView : MonoBehaviour, IWindowView{
		[SerializeField] private Image _imageFader;
		[SerializeField] private GameObject _loadingIcon;

		public void Initialize(){
			
		}

		public void OnOpen(){
			if(_loadingIcon!=null) _loadingIcon.SetActive(false);
			_imageFader.color = Color.clear;
			_imageFader.DOColor(Color.black, 1f/*GameConfig.LoadingFadeInOutTime*/).OnComplete(() => {
				if(_loadingIcon!=null) _loadingIcon?.SetActive(true);
			});
		}

		public void OnClose(){
			if(_loadingIcon!=null) _loadingIcon.SetActive(false);
			_imageFader.DOColor(Vector4.zero, 1f/*GameConfig.LoadingFadeInOutTime*/);
		}
	}
}