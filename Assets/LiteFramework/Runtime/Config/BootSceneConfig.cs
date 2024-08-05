using Trisibo;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace LiteFramework.Runtime.Config
{
    public class BootSceneConfig : GameConfig
    {
        
        [SerializeField]
        private SceneField _startScene;
        public int BootSceneBuildIndex => _startScene.BuildIndex;
#if UNITY_EDITOR
        public SceneAsset BootScene => _startScene?.EditorSceneAsset;
        [HideInInspector] public SceneAsset PrevScene;
#endif
    }
}