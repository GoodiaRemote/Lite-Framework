#if UNITY_EDITOR
using System.Collections;
using UnityEditor;
using UnityEngine;

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

        private static BuildTargetGroup[] GetBuildTargets()
        {
            var _targetGroupList = new ArrayList();
            _targetGroupList.Add(BuildTargetGroup.Android);
            _targetGroupList.Add(BuildTargetGroup.iOS);
            _targetGroupList.Add(BuildTargetGroup.Standalone);
            _targetGroupList.Add(BuildTargetGroup.WSA);
            _targetGroupList.Add(BuildTargetGroup.WebGL);
            return (BuildTargetGroup[])_targetGroupList.ToArray(typeof(BuildTargetGroup));
        }
        
        static void SetScriptingDefineSymbols()
        {
            BuildTargetGroup[] _buildTargets = GetBuildTargets();
            if (!EditorPrefs.GetBool(Application.dataPath+"Project_opened"))
            {
                foreach (BuildTargetGroup _target in _buildTargets)
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(_target, "");
                }
                EditorPrefs.SetBool(Application.dataPath+"Project_opened",true);
            }
            foreach (BuildTargetGroup _target in _buildTargets)
            {
                string defines = PlayerSettings.GetScriptingDefineSymbolsForGroup(_target);
                AddDefine(ref defines, "UNITASK_DOTWEEN_SUPPORT");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(_target, defines);
            }
        }
        
        private static void AddDefine(ref string defines, string symbols)
        {
            if (!defines.Contains(symbols))
            {
        	    defines = defines + "; " + symbols;
            }
        }
    }
}

#endif

