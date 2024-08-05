using LiteFramework.Editor.Utils;
using LiteFramework.Runtime.Audio;
using LiteFramework.Runtime.Config;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace LiteFramework.Editor.Config
{   
    public class ConfigWindow : OdinMenuEditorWindow
    {
        [MenuItem("Lite Framework/GameConfig", false, -1)]
        private static void ShowWindow()
        {
            GetWindow<ConfigWindow>("Game Config").Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree
            {
                { "Boot Scene", EditorConfigHelper.GetOrCreateConfig<BootSceneConfig>() },
                { "Audio", EditorConfigHelper.GetOrCreateConfig<AudioConfig>() }
            };

            return tree;
        }
    }
}