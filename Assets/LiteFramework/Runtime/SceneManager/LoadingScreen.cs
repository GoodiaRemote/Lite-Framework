using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LiteFramework.Runtime.SceneManager
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _transitionCanvasGroup;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Image _loadingBar;
        
        public float Progress => _loadingBar.fillAmount;

        private void OnEnable()
        {
            _loadingScreen.SetActive(false);
            _loadingBar.fillAmount = 0;
            _transitionCanvasGroup.alpha = 0;
        }

        public void SetShowLoadingBg(bool show)
        {
            _loadingScreen.SetActive(show);
        }

        public void UpdateLoadingBar(float progress)
        {
            _loadingBar.fillAmount = progress;
        }
        
        public async UniTask FadeIn(float transitionDuration = 1)
        {
            _transitionCanvasGroup.blocksRaycasts = true;
            await _transitionCanvasGroup.DOFade(1, transitionDuration).SetUpdate(true);
        }
        
        public async UniTask FadeOut(float transitionDuration = 1, float delay = 0)
        {
            await _transitionCanvasGroup.DOFade(0, transitionDuration).SetDelay(delay).SetUpdate(true);
            _transitionCanvasGroup.blocksRaycasts = false;
        }
    }
}