using System.Collections.Concurrent;
using System.Collections.Generic;
using hubbl.web.models;

namespace hubbl.web
{
    public class Realtime
    {
        public ConcurrentDictionary<string, string> connectedUsers;
        public ConcurrentDictionary<string, List<string>> hubUsers; // TODO: fix concurrency in list

        private Realtime()
        {
        }

        private static Realtime instance;

        public static Realtime getInstance()
        {
            return instance ?? (instance = new Realtime());
        }
    }
}