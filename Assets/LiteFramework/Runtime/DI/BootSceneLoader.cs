using LiteFramework.Runtime.Base;
using LiteFramework.Runtime.Scene;
using Reflex.Attributes;
using Trisibo;
using UnityEngine;

namespace LiteFramework.Runtime.DI
{
    public class BootSceneLoader : BaseMonoBehaviour
    {
        [SerializeField] private SceneField _mainScene;
        [Inject] private ISceneManager _sceneManager;
        
        private void Awake()
        {
            _sceneManager.LoadSceneWithLoadingScreen(_mainScene.BuildIndex);
        }
    }
}