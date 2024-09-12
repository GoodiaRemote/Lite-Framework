using LiteFramework.Editor.Utils;
using LiteFramework.Runtime.Audio;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace LiteFramework.Editor.Config
{   
    public class ConfigWindow : OdinMenuEditorWindow
    {
        [MenuItem("Lite Framework/Menu", false, -1)]
        private static void ShowWindow()
        {
            GetWindow<ConfigWindow>("Lite Framework").Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree
            {
                { "Audio", EditorConfigHelper.GetOrCreateConfig<AudioConfig>() }
            };

            return tree;
        }
    }
}