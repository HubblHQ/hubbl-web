using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using hubbl.web.models;

namespace hubbl.web
{
    public class Realtime
    {
        private ConcurrentDictionary<string, string> connectedUsers;
        private ConcurrentDictionary<string, List<string>> hubUsers;

        public string getUserHub(string user) {
                return connectedUsers[user];
        }

        public bool isUserConnected(string user) {
            return getUserHub(user) != null;
        }

        public void connectToHub(string user, string hub)
        {
            if (isUserConnected(user)) throw new ApplicationException("User is already connected to hub!");

            connectedUsers[user] = hub;
            var userList = hubUsers[hub] ?? (hubUsers[hub] = new List<string>());

            lock (userList)
            {
                userList.Add(user);
            }
        }

        public void disconnect(string user)
        {
            if (!isUserConnected(user)) throw new ApplicationException("User is not connected to hub!");

            string hub = connectedUsers[user];
            connectedUsers[user] = null;

            var userList = hubUsers[hub];
            lock (userList)
            {
                userList.Remove(user);
            }
        }

        public List<string> getHubUsers(string hub) {
            var list = connectedUsers[hub];
            lock (list) {
                return (List<string>)list.Clone();
            }
        }

        public bool isUserConnectedToHub(string user, string hub) {
            var list = connectedUsers[hub];
            lock (list) {
                return list.Contains(user);
            }
        }

        private Realtime() { }

        private static Realtime instance;

        public static Realtime getInstance()
        {
            return instance ?? (instance = new Realtime());
        }
    }
}