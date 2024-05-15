using System;
using System.Collections.Generic;

namespace SGEngine.App
{
    public static class DI
    {
        private static Dictionary<Type, object> _container = new();

        public static void Add<T>(T t) where T : class
        {
            var type = typeof(T);

            if (_container.ContainsKey(type)) return;

            _container.Add(type, t);
        }

        public static T Get<T>() where T : class
        {
            var type = typeof(T);

            return _container[type] as T;
        }
    }
}