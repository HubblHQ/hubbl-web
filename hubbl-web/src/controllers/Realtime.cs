using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using hubbl.web.models;

namespace hubbl.web {
    public class Realtime {
        #region users and hubs

        private ConcurrentDictionary<string, string> connectedUsers;
        private ConcurrentDictionary<string, List<string>> hubUsers;

        public string getUserHub(string user) {
            return connectedUsers[user];
        }

        public bool isUserConnected(string user) {
            return getUserHub(user) != null;
        }

        public void connectToHub(string user, string hub) {
            if (isUserConnected(user)) throw new ApplicationException("User is already connected to hub!");

            connectedUsers[user] = hub;
            List<string> userList = hubUsers[hub] ?? (hubUsers[hub] = new List<string>());

            lock (userList) {
                userList.Add(user);
            }
        }

        public void disconnect(string user) {
            if (!isUserConnected(user)) throw new ApplicationException("User is not connected to hub!");

            string hub = connectedUsers[user];
            connectedUsers[user] = null;

            List<string> userList = hubUsers[hub];
            lock (userList) {
                userList.Remove(user);
            }
        }

        public List<string> getHubUsers(string hub) {
            string list = connectedUsers[hub];
            lock (list) {
                return (List<string>) list.Clone();
            }
        }

        public bool isUserConnectedToHub(string user, string hub) {
            string list = connectedUsers[hub];
            lock (list) {
                return list.Contains(user);
            }
        }

        #endregion


        #region playlists

        private ConcurrentDictionary<string, Player> players = new ConcurrentDictionary<string, Player>();

        public Player getPlayer(string userId) {
            return players[userId];
        }

        public List<long> getPlayList(string userId) {
            Player p = players[userId];
            return p == null ? null : players[userId].getPlayList();
        }

        #endregion

        private Realtime() { }

        private static Realtime instance;

        public static Realtime getInstance() {
            return instance ?? (instance = new Realtime());
        }
    }
}