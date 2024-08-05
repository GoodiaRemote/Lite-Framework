using System;
using System.Collections.Generic;
using LiteFramework.Runtime.ObjectPool;
using Reflex.Attributes;
using UnityEngine;

namespace LiteFramework.Runtime.Audio
{
    public class UnityAudioManger : MonoBehaviour, IAudioManager
    {
        [SerializeField] private AudioObject _audioObjectPrefab;
        [SerializeField] private AudioObject _musicSourcePrefab;
        private AudioObject _musicSource;
        private Pool<AudioObject> _pool;
        private bool _mute;
        private float _musicVolume = 1;
        private float _sfxVolume = 1;
        private readonly Dictionary<string, AudioObject> _lookUpLoopSfx = new ();
        
        [Inject] private AudioConfig _audioConfig;

        public event Action<bool> OnMute;
        public event Action<float> OnSfxVolumeChange;
        public event Action<float> OnMusicVolumeChange;

        public void Awake()
        {
            _pool = new Pool<AudioObject>(_audioObjectPrefab, 100, 1000);
            _musicSource = Instantiate(_musicSourcePrefab);
            _musicSource.Parent = transform;
        }

        public void SetMute(bool mute)
        {
            _mute = mute;
            _musicSource.Mute = mute;
            OnMute?.Invoke(mute);

        }

        public bool ToggleMute()
        {
            bool mute = !_mute;
            SetMute(mute);
            return mute;
        }
        
        public void SetMusicVolume(float volume)
        {
            _musicSource.Volume = volume;
            _musicVolume = _musicSource.Volume;
            OnMusicVolumeChange?.Invoke(_musicVolume);
        }

        public void SetSfxVolume(float volume)
        {
            _sfxVolume = Mathf.Clamp01(volume);
            OnSfxVolumeChange?.Invoke(_sfxVolume);
        }


        public void PlaySfx(string audioName)
        {
            if (_audioConfig.Sounds.TryGetValue(audioName, out var clip))
            {
                var audioObject = _pool.Get();
                audioObject.Parent = transform;
                audioObject.Mute = _mute;
                audioObject.Volume = _sfxVolume;
                audioObject.Play(clip);
            }
        }

        public string PlaySfxLoop(string audioName)
        {
            if (_audioConfig.Sounds.TryGetValue(audioName, out var clip))
            {
                var audioObject = _pool.Get();
                audioObject.Parent = transform;
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
            _musicSource.Pause();
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }
    }
}