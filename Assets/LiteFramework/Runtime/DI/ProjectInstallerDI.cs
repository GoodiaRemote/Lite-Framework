using LiteFramework.Runtime.Audio;
using LiteFramework.Runtime.SceneManager;
using Reflex.Core;
using UnityEngine;

namespace LiteFramework.Runtime.DI
{
    public class ProjectInstallerDI : MonoBehaviour, IInstaller
    {
        [SerializeField] private ScriptableObject _audioManagerSO;
        [SerializeField] private ScriptableObject _sceneManagerSO;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_audioManagerSO, typeof(IAudioManager));
            containerBuilder.AddSingleton(_sceneManagerSO, typeof(ISceneManager));
        }
    }
}