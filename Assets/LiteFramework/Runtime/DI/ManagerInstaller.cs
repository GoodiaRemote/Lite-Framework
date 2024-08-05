using LiteFramework.Runtime.Audio;
using LiteFramework.Runtime.Scene;
using Reflex.Core;
using UnityEngine;

namespace LiteFramework.Runtime.DI
{
    public class ManagerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private GameObject _audioManager;
        [SerializeField] private GameObject _sceneManager;

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_audioManager.GetComponent<IAudioManager>(), typeof(IAudioManager));
            containerBuilder.AddSingleton(_sceneManager.GetComponent<ISceneManager>(), typeof(ISceneManager));
        }

        
    }
}