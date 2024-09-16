using System;
using UnityEngine.SceneManagement;

namespace LiteFramework.Runtime.SceneManager
{
    public interface ISceneManager
    {
        public Action<Scene> OnSceneLoaded { get; set; }
        public void LoadScene(int sceneIndex, float transitionDuration = 1f,Action<Scene> sceneLoaded = null);
        public void LoadSceneWithLoadingScreen(int sceneIndex, float transitionDuration = 1f, Action<Scene> sceneLoaded = null);
    }
}