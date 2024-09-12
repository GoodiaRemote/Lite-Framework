// using System;
// using LiteFramework.Editor.Utils;
// using LiteFramework.Runtime.Config;
// using UnityEditor;
// using UnityEditor.SceneManagement;
//
// namespace LiteFramework.Editor
// {
//     [InitializeOnLoad]
//     public static class BootScene
//     {
//         static BootScene()
//         {
//             EditorApplication.playModeStateChanged += LoadDefaultScene;
//         }
//         
//         [MenuItem("Lite Framework/Boot Scene/Play")]
//         private static void PlayBootScene()
//         {
//             var prevScenePath = EditorSceneManager.GetActiveScene().path;
//             var config = EditorConfigHelper.GetOrCreateConfig<BootSceneConfig>();
//             config.PrevScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(prevScenePath);
//             EditorApplication.EnterPlaymode();
//         }
//
//         private static void LoadDefaultScene(PlayModeStateChange state)
//         {
//             var config = EditorConfigHelper.GetOrCreateConfig<BootSceneConfig>();
//             if(config.PrevScene == null) return;
//             switch (state)
//             {
//                 case PlayModeStateChange.EnteredEditMode:
//                     EditorSceneManager.playModeStartScene = null;
//                     config.PrevScene = null;
//                     break;
//                 case PlayModeStateChange.ExitingEditMode:
//                     if (config.PrevScene != config.BootScene)
//                     {
//                         EditorSceneManager.playModeStartScene = config.BootScene;
//                     }
//                     break;
//                 case PlayModeStateChange.EnteredPlayMode:
//                     break;
//                 case PlayModeStateChange.ExitingPlayMode:
//                     break;
//                 default:
//                     throw new ArgumentOutOfRangeException(nameof(state), state, null);
//             }
//         }
//     }
// }