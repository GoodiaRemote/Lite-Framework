using LiteFramework.Runtime.Audio;
using Reflex.Core;
using UnityEngine;

namespace LiteFramework.Runtime.DI
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private AudioConfig _audioConfig;
        
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_audioConfig);
        }
    }
}