using System.Collections.Generic;
using System;

namespace ShootEmUpZenject
{
    public sealed class EventBus
    {
        private readonly Dictionary<string, List<object>> _signalCallbackList = new();

        public void Subscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;
            if (_signalCallbackList.ContainsKey(key))
            {
                _signalCallbackList[key].Add(callback);
            }
            else
            {
                _signalCallbackList.Add(key, new List<object>() { callback });
            }
        }

        public void UnSubscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;
            if (_signalCallbackList.ContainsKey(key))
            {
                _signalCallbackList[key].Remove(callback);
            }
        }

        public void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;
            if (_signalCallbackList.ContainsKey(key))
            {
                foreach (var obj in _signalCallbackList[key])
                {
                    var callback = obj as Action<T>;
                    callback?.Invoke(signal);
                }
            }
        }
    }
}