#if UNITY_EDITOR
using System.Linq;
using UnityEditor;

namespace LiteFramework.Editor
{
    public class PostImporting : AssetPostprocessor
    {
        private static bool _imported = false;

        private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets,
            string[] movedFromAssetPaths)
        {
            SetScriptingDefineSymbols();
        }
        
        
        static void SetScriptingDefineSymbols()
        {
            var scriptingDefinesString =
                PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            var scriptingDefinesStringHashSet = scriptingDefinesString.Split(';').ToHashSet();
                scriptingDefinesStringHashSet.Add("UNITASK_DOTWEEN_SUPPORT");
                scriptingDefinesStringHashSet.Add("DOTWEEN");
                scriptingDefinesStringHashSet.Add("ODIN_INSPECTOR");
                scriptingDefinesStringHashSet.Add("ODIN_INSPECTOR_3");
                scriptingDefinesStringHashSet.Add("ODIN_INSPECTOR_3_1");
                scriptingDefinesStringHashSet.Add("LITE_FRAMEWORK");
            
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", scriptingDefinesStringHashSet.ToArray()));
        }
    }
}

#endif

