using System;
using LiteFramework.Runtime.Base;
using LiteFramework.Runtime.ObjectPool;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;

namespace LiteFramework.Runtime.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioObject : BaseMonoBehaviour, IPoolale<AudioObject>
    {
        [SerializeField] private bool _releaseOnStop;
        private AudioSource _source;
        private bool _alive;
        private float _volume = 1;
        public event Action<AudioObject> ReleaseEvent;
        
        [Inject] private IAudioManager _audioManager;

        public bool Mute
        {
            get => _source.mute;
            set => _source.mute = value;
        }
        public float Volume
        {
            get => _volume;
            set {
                _volume = Mathf.Clamp01(value);
                _source.volume = _volume;
            } 
        }


        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            AttributeInjector.Inject(this, gameObject.scene.GetSceneContainer());
        }


        public void OnGet()
        {
            _audioManager.OnMute += OnMute;
            _audioManager.OnSfxVolumeChange += OnVolumeChange;
            _alive = true;
            _source.volume = Volume;
        }

        public void OnRelease()
        {
            _audioManager.OnMute -= OnMute;
            _audioManager.OnSfxVolumeChange -= OnVolumeChange;
            _alive = false;
            _source.clip = null;
            _source.loop = false;
        }

        public void ReleaseToPool()
        {
            ReleaseEvent?.Invoke(this);
        }

        private void Update()
        {
            if (_releaseOnStop && _alive && !_source.isPlaying)
            {
                ReleaseToPool();
            }
        }

        private void OnMute(bool mute)
        {
            Mute = mute;
        }

        private void OnVolumeChange(float volume)
        {
            Volume = volume;
        }

        public void Play(AudioClip clip, bool loop = false)
        {
            _source.Stop();
            _source.clip = clip;
            _source.loop = loop;
            _source.Play();
        }

        public void Pause()
        {
            _source.Pause();
        }

        public void Stop()
        {
            _source.Stop();
        }
    }
}