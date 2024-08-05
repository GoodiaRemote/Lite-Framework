#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace LiteFramework.Editor
{
    [InitializeOnLoad]
    public class InitDefineSymbols
    {
        public static readonly string [] Symbols = new string[] {
            "UNITASK_DOTWEEN_SUPPORT",
        };
        
        static InitDefineSymbols ()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup ( EditorUserBuildSettings.selectedBuildTargetGroup );
            List<string> allDefines = definesString.Split ( ';' ).ToList ();
            allDefines.AddRange ( Symbols.Except ( allDefines ) );
            PlayerSettings.SetScriptingDefineSymbolsForGroup (
                EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join ( ";", allDefines.ToArray () ) );
        }
    }
}
#endif