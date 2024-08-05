using System;

namespace LiteFramework.Runtime.Audio
{
    public interface IAudioManager
    {
        public event Action<bool> OnMute;
        public event Action<float> OnSfxVolumeChange;
        public event Action<float> OnMusicVolumeChange;

        public void SetMute(bool mute);
        public bool ToggleMute();
        public void SetMusicVolume(float volume);
        public void SetSfxVolume(float volume);
        public void PlaySfx(string audioName);
        public string PlaySfxLoop(string audioName);
        public void StopSfxLoop(string id);
        public void PlayMusic(string audioName,  bool loop = true);
        public void PauseMusic();
        public void StopMusic();
    }
}