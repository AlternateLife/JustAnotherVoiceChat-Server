using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using AlternateVoice.Server.GTMP.Elements;
using AlternateVoice.Server.GTMP.Exceptions;
using AlternateVoice.Server.GTMP.Interfaces;

namespace AlternateVoice.Server.GTMP
{
    public class GtmpFactory
    {

        private static ConcurrentDictionary<Type, IGtmpVoiceElement> _dependencies;

        public static T Make<T>() where T : class, IGtmpVoiceElement
        {
            var type = typeof(T);

            if (type == typeof(IVoiceToClientMapper))
            {
                return new VoiceToClientMapper() as T;
            }

            return default(T);
        }

        public static T Get<T>() where T : class, IGtmpVoiceElement
        {
            if (_dependencies == null)
            {
                _dependencies = new ConcurrentDictionary<Type, IGtmpVoiceElement>();
            }

            var elemenType = typeof(T);
            IGtmpVoiceElement result;
            
            if (_dependencies.TryGetValue(elemenType, out result))
            {
                return (T) result;
            }

            result = Make<T>();
            if (!_dependencies.TryAdd(elemenType, result))
            {
                throw new GtmpElementCreationException(elemenType);
            }

            return (T) result;
        }
    }
}
