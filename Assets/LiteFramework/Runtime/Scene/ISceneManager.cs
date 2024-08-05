using System;

namespace LiteFramework.Runtime.Scene
{
    public interface ISceneManager
    {
        public void LoadScene(int sceneIndex, float transitionDuration = 1f,Action<UnityEngine.SceneManagement.Scene> sceneLoaded = null);
        public void LoadSceneWithLoadingScreen(int sceneIndex, float transitionDuration = 1f, Action<UnityEngine.SceneManagement.Scene> sceneLoaded = null);
    }
}