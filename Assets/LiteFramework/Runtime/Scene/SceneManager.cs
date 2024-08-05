using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using LiteFramework.Runtime.Utils;
using LiteFramework.Runtime.Base;
using Reflex.Core;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LiteFramework.Runtime.Scene
{
    public class SceneManager : BaseMonoBehaviour, ISceneManager
    {
        [SerializeField] private CanvasGroup _transitionCanvasGroup;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Image _loadingBar;
        
        private int _currentScene;
        private float _progressValue;
        
        private void Awake()
        {
            _transitionCanvasGroup.alpha = 0;
        }

        private AsyncOperation LoadSceneInternal(int sceneIndex)
        {
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex,  LoadSceneMode.Additive);
        }
        
        private async UniTask UnLoadSceneInternal(int sceneIndex)
        {
            // build scene index 0 is boot scene we need keep that scene 
            if(sceneIndex == 0) return;
            await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneIndex);
        }

        private UnityEngine.SceneManagement.Scene GetNewSceneLoaded()
        {
            return UnityEngine.SceneManagement.SceneManager.GetSceneAt(UnityEngine.SceneManagement.SceneManager.sceneCount - 1);
        }

        public async void LoadScene(int sceneIndex, float transitionDuration = 1f, Action<UnityEngine.SceneManagement.Scene> sceneLoaded = null)
        {
            _loadingBar.fillAmount = 0;
            await FadeIn(transitionDuration);
            var asyncOperation = LoadSceneInternal(sceneIndex);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    UnLoadSceneInternal(_currentScene).UnAwait();
                    asyncOperation.allowSceneActivation = true;
                    var newScene = GetNewSceneLoaded();
                    ReflexSceneManager.OverrideSceneParentContainer(newScene, gameObject.scene.GetSceneContainer());
                    sceneLoaded?.Invoke(newScene);
                    _currentScene = sceneIndex;
                }
                await UniTask.Yield();
            }

            UnityEngine.SceneManagement.SceneManager.SetActiveScene(GetNewSceneLoaded());
            await FadeOut(transitionDuration, 0.3f);
        }

        public async void LoadSceneWithLoadingScreen(int sceneIndex, float transitionDuration = 1f, Action<UnityEngine.SceneManagement.Scene> sceneLoaded = null)
        {
            _loadingBar.fillAmount = 0;
            await FadeIn(transitionDuration);
            _loadingScreen.SetActive(true);
            var asyncOperation = LoadSceneInternal(sceneIndex);
            asyncOperation.allowSceneActivation = false;
            await FadeOut(transitionDuration);
            while (!asyncOperation.isDone || _loadingBar.fillAmount < 1f)
            {
                var progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                _loadingBar.fillAmount = Mathf.Lerp(_loadingBar.fillAmount, progress, Time.unscaledDeltaTime * 30);
                if (_loadingBar.fillAmount >= 0.99f)
                {
                    _loadingBar.fillAmount = 1;
                    await FadeIn(transitionDuration);
                    UnLoadSceneInternal(_currentScene).UnAwait();
                    asyncOperation.allowSceneActivation = true;
                    var newScene = GetNewSceneLoaded();
                    ReflexSceneManager.OverrideSceneParentContainer(newScene, gameObject.scene.GetSceneContainer());
                    sceneLoaded?.Invoke(newScene);
                    _currentScene = sceneIndex;
                    _loadingScreen.SetActive(false);
                }

                await UniTask.Yield();
            }
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(GetNewSceneLoaded());
            await FadeOut(transitionDuration, 0.3f);

        }

        private async UniTask FadeIn(float transitionDuration = 1)
        {
            _transitionCanvasGroup.blocksRaycasts = true;
            await _transitionCanvasGroup.DOFade(1, transitionDuration).SetUpdate(true);
        }
        
        private async UniTask FadeOut(float transitionDuration = 1, float delay = 0)
        {
            await _transitionCanvasGroup.DOFade(0, transitionDuration).SetDelay(delay).SetUpdate(true);
            _transitionCanvasGroup.blocksRaycasts = false;
        }
        
    }
}