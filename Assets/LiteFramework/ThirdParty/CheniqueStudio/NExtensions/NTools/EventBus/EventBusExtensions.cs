using System;

namespace CheniqueStudio.NExtensions.NTools.EventBus
{
    public static class EventBusExtensions
    {
        public static void Register<T>(out EventBinding<T> eventBinding, Action<T> onEvent) where T : IEvent
        {
            eventBinding = new EventBinding<T>(onEvent);
            EventBus<T>.Register(eventBinding);
        }
        
        public static void Register<T>(out EventBinding<T> eventBinding, Action onEvent) where T : IEvent
        {
            eventBinding = new EventBinding<T>(onEvent);
            EventBus<T>.Register(eventBinding);
        }
        
        public static void Deregister<T>(ref EventBinding<T> eventBinding) where T : IEvent
        {
            EventBus<T>.Deregister(eventBinding);
        }
    }
}