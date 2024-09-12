using System;
using Cysharp.Threading.Tasks;
using LiteFramework.Runtime.Event.Type;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LiteFramework.Runtime.SceneManager
{
    [CreateAssetMenu(menuName = "LiteFramework/Manager/Scene")]
    public class SceneManager : ScriptableObject, ISceneManager
    {
        [SerializeField] private VoidEvent _sceneLoadedEvent;
        [SerializeField] private LoadingScreen _loadingScreenPrefab;

        
        private int _currentScene;
        private float _progressValue;
        
        public Action<Scene> OnSceneLoaded { get; set; }

        private AsyncOperation LoadSceneInternal(int sceneIndex)
        {
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex,  LoadSceneMode.Single);
        }

        private Scene GetNewSceneLoaded()
        {
            return UnityEngine.SceneManagement.SceneManager.GetSceneAt(UnityEngine.SceneManagement.SceneManager.sceneCount - 1);
        }

        public async void LoadScene(int sceneIndex, float transitionDuration = 1f, Action<Scene> sceneLoaded = null)
        {
            var loadingScreen = Instantiate(_loadingScreenPrefab, null);
            DontDestroyOnLoad(loadingScreen);
            await loadingScreen.FadeIn(transitionDuration);
            var asyncOperation = LoadSceneInternal(sceneIndex);
            asyncOperation.allowSceneActivation = false;
            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    asyncOperation.allowSceneActivation = true;
                }
                await UniTask.Yield();
            }

            var newScene = GetNewSceneLoaded();
            sceneLoaded?.Invoke(newScene);
            OnSceneLoaded?.Invoke(newScene);
            _sceneLoadedEvent.Raise();
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(GetNewSceneLoaded());
            _currentScene = sceneIndex;
            await loadingScreen.FadeOut(transitionDuration, 0.3f);
            Destroy(loadingScreen.gameObject);
        }

        public async void LoadSceneWithLoadingScreen(int sceneIndex, float transitionDuration = 1f, Action<Scene> sceneLoaded = null)
        {
            var loadingScreen = Instantiate(_loadingScreenPrefab, null);
            DontDestroyOnLoad(loadingScreen);
            await loadingScreen.FadeIn(transitionDuration);
            loadingScreen.SetShowLoadingBg(true);
            var asyncOperation = LoadSceneInternal(sceneIndex);
            asyncOperation.allowSceneActivation = false;
            await loadingScreen.FadeOut(transitionDuration);
            while (!asyncOperation.isDone || loadingScreen.Progress < 1f)
            {
                var progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
                var fillAmount = Mathf.Lerp(loadingScreen.Progress, progress, Time.unscaledDeltaTime * 30);
                loadingScreen.UpdateLoadingBar(fillAmount);
                if (fillAmount >= 0.99f)
                {
                    loadingScreen.UpdateLoadingBar(1);
                    asyncOperation.allowSceneActivation = true;
                }

                await UniTask.Yield();
            }
            await loadingScreen.FadeIn(transitionDuration);
            var newScene = GetNewSceneLoaded();
            sceneLoaded?.Invoke(newScene);
            OnSceneLoaded?.Invoke(newScene);
            _sceneLoadedEvent.Raise();
            UnityEngine.SceneManagement.SceneManager.SetActiveScene(GetNewSceneLoaded());
            _currentScene = sceneIndex;
            loadingScreen.SetShowLoadingBg(false);
            await loadingScreen.FadeOut(transitionDuration, 0.2f);
            Destroy(loadingScreen.gameObject);
        }
        
    }
}