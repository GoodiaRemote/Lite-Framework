using System;
using System.Collections.Generic;
using LiteFramework.Runtime.Event.Type;
using LiteFramework.Runtime.ObjectPool;
using LiteFramework.Runtime.Utils;
using UnityEngine;

namespace LiteFramework.Runtime.Audio
{
    [CreateAssetMenu(menuName = "LiteFramework/Manager/Audio")]
    public class UnityAudioManger : ScriptableObject, IAudioManager
    {
        [SerializeField] private VoidEvent _sceneLoadedEvent;
        [SerializeField] private AudioObject _audioObjectPrefab;
        [SerializeField] private AudioObject _musicSourcePrefab;
        
        //for some reason (???) Unity keep Serialize private property in SO, so NonSerialized must use for prevent that!
        [NonSerialized] private AudioObject _musicSource;
        [NonSerialized] private Pool<AudioObject> _pool;
        [NonSerialized] private bool _initialized;
        [NonSerialized] private bool _mute;
        [NonSerialized] private float _musicVolume = 1;
        [NonSerialized] private float _sfxVolume = 1;
        [NonSerialized] private AudioConfig _audioConfig;
        [NonSerialized] private readonly Dictionary<string, AudioObject> _lookUpLoopSfx = new ();
        
        public event Action<bool> OnMute;
        public event Action<float> OnSfxVolumeChange;
        public event Action<float> OnMusicVolumeChange;

        
        public void OnEnable()
        {
            _audioConfig = ConfigHelper.GetConfig<AudioConfig>();
        }

        private void TryInit()  
        {
            if(_initialized) return;
            _initialized = true;
            _pool = new Pool<AudioObject>(_audioObjectPrefab, 100);
            _musicSource = Instantiate(_musicSourcePrefab, null);
            DontDestroyOnLoad(_musicSource);
            _sceneLoadedEvent.Register(_pool.Clear);
        }

        public void SetMute(bool mute)
        {
            if(!Application.isPlaying) return;
            TryInit();
            _mute = mute;
            _musicSource.Mute = mute;
            OnMute?.Invoke(mute);

        }

        public bool ToggleMute()
        {
            if(!Application.isPlaying) return _mute;
            TryInit();
            bool mute = !_mute;
            SetMute(mute);
            return mute;
        }
        
        public void SetMusicVolume(float volume)
        {
            if(!Application.isPlaying) return;
            TryInit();
            _musicSource.Volume = volume;
            _musicVolume = _musicSource.Volume;
            OnMusicVolumeChange?.Invoke(_musicVolume);
        }

        public void SetSfxVolume(float volume)
        {
            if(!Application.isPlaying) return;
            TryInit();
            _sfxVolume = Mathf.Clamp01(volume);
            OnSfxVolumeChange?.Invoke(_sfxVolume);
        }


        public void PlaySfx(string audioName)
        {
            if(!Application.isPlaying) return;
            TryInit();
            if (_audioConfig.Sounds.TryGetValue(audioName, out var clip))
            {
                var audioObject = _pool.Get();
                Debug.Log(_pool.CountAll);
                audioObject.Mute = _mute;
                audioObject.Volume = _sfxVolume;
                audioObject.Play(clip);
            }
        }

        public string PlaySfxLoop(string audioName)
        {
            if(!Application.isPlaying) return string.Empty;
            TryInit();
            if (_audioConfig.Sounds.TryGetValue(audioName, out var clip))
            {
                var audioObject = _pool.Get();
                audioObject.Mute = _mute;
                audioObject.Volume = _sfxVolume;
                audioObject.Play(clip, true);
                var id = Guid.NewGuid().ToString();
                _lookUpLoopSfx.TryAdd(id, audioObject);

                return id;
            }

            return string.Empty;
        }

        public void StopSfxLoop(string id)
        {
            if(!Application.isPlaying) return;
            TryInit();
            if (_lookUpLoopSfx.TryGetValue(id, out var audioObject))
            {
                audioObject.Stop();
                _lookUpLoopSfx.Remove(id);
            }
        }

        public void PlayMusic(string audioName, bool loop = true)
        {
            if (_audioConfig.Musics.TryGetValue(audioName, out var clip))
            {
                _musicSource.Stop();
                _musicSource.Mute = _mute;
                _musicSource.Volume = _musicVolume;
                _musicSource.Play(clip, loop);
            }
        }

        public void PauseMusic()
        {
            if(!Application.isPlaying) return;
            TryInit();
            _musicSource.Pause();
        }

        public void StopMusic()
        {
            if(!Application.isPlaying) return;
            TryInit();
            _musicSource.Stop();
        }
    }
}