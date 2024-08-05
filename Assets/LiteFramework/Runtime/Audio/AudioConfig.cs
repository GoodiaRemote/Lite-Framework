using System.Collections.Generic;
using LiteFramework.Runtime.Config;
using LiteFramework.Runtime.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
#endif

namespace LiteFramework.Runtime.Audio
{
    public class AudioConfig : GameConfig
    {

        [SerializeField, Required, FolderPath(RequireExistingPath = true, ParentFolder = "Assets")]
        private string _soundDefineFileSavePath;
        
        [TabGroup("Musics"), Searchable]
        public readonly Dictionary<string, AudioClip> Musics = new();
        [TabGroup("Sounds"), Searchable]
        public readonly Dictionary<string, AudioClip> Sounds = new();

        #region Editor

#if UNITY_EDITOR
        [Button(ButtonSizes.Gigantic, Icon = SdfIconType.GearFill), GUIColor(0, 1, 0)]
        private void GenerateAudioDefine()
        {
            
            var backgroundMusicBody = "";
            backgroundMusicBody = Musics.Aggregate(backgroundMusicBody, (current, music) => current + $"\t\tpublic const string {music.Key} = \"{music.Key}\";\n");
            var soundEffectsBody = "";
            soundEffectsBody = Sounds.Aggregate(soundEffectsBody, (current, music) => current + $"\t\tpublic const string {music.Key} = \"{music.Key}\";\n");


            var ns = _soundDefineFileSavePath.Replace('/', '.').Replace(".Scripts", "");
            
            var template = TemplateHelper.GetTemplate("AudioDefine", new Dictionary<string, string>
            {
                {"backgroundMusicBody", backgroundMusicBody},
                {"soundEffectsBody", soundEffectsBody},
                {"namespace", ns}
            });

            using (var sw = new StreamWriter(Path.Combine(Application.dataPath, _soundDefineFileSavePath,$"AudioDefine.cs")))
            {
                sw.Write (template);
            }

            AssetDatabase.Refresh (ImportAssetOptions.ForceUpdate);
        }
#endif

        #endregion
    }
}