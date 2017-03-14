using System.Collections.Generic;
using hubbl.web.models;
using hubbl.web.models.network;
using MongoDB.Bson;
using Nancy.ModelBinding;

namespace hubbl.web {

    // Caution! Oneliners ahead!
	public class Handlers : Nancy.NancyModule {

		public Handlers () {

			// get webapp page
			Get("/", parameters => View["index.html"]);

			initAwailableForAll();
			initAwailableForAutentificated();
			initAwailableForConnected();
		}
			
		private void initAwailableForAll() {
			
			// { login, password } -> { id, name, sessionToken }
			Get("/signin", _ => User.toLoginResponse(
			    User.tryLogin(Request.Query["login"], Request.Query["password"])));

			// { name, login, password } -> {}
			Get("/signup", _ => User.toSignUpResponse(
			    User.trySignUp(Request.Query["name"], Request.Query["login"], Request.Query["password"])));

		}

		private void initAwailableForAutentificated() {
            // required data for all: token

			// {} -> [ { id, name, author }, ... ]
			Get("/hubs", _ => User.authentificated(Request.Query["token"]) ?
		        Hub.getAll().ToJson() : new ErrorResponse(300, Constants.NetMsg.FORBIDDEN).ToJson());

            // { query } -> [ { id, name, author }, ... ]
            // FUCKING C# decided that one uses find as dynamic call. Can be fixed by cast. No idea why it is happening. Doesn't depends on what's inside of find.
		    Get("/hubs/search", _ => User.authentificated(Request.Query["token"]) ?
		        ((List<HubInfo>)Hub.find(Request.Query["query"])).ToJson() : new ErrorResponse(300, Constants.NetMsg.FORBIDDEN).ToJson());

		    // { id } -> { id, name, author }
			Get("/hub", _ => User.authentificated(Request.Query["token"]) ?
		        Hub.getOrError(Request.Query["id"]) : new ErrorResponse(300, Constants.NetMsg.FORBIDDEN).ToJson());

			// { id } -> {}
			Get("/hub/connect", _ => Hub.tryConnect(Request.Query["id"], Request.Query["token"]));

		    // { name } -> { id }
			Get("/hub/new", _ => Hub.tryCreate(Request.Query["token"], Request.Query["name"]));

		}

		private void initAwailableForConnected() {

		    // { id } -> {}
		    Get("/hub/disconnect", _ => "");

		    // {} -> {}
			Get("/hub/delete", _ => "");

			// { } -> { users: [ { id, name }, ... ] }
			Get("/hub/users", _ => Hub.getUsers(Request.Query["token"]));

			// { } -> { name }
			Get("/user", _ => User.getUser(User.get(Request.Query["token"])));

			// {} -> { playlist: [ ... ] }
			Get("/playlist", _ => Player.getPlayListResponse(Request.Query["token"]));


			// {} -> { playlist: [ ... ] }
			Get("/playlist/full", _ => "");

			// { id } -> { ... }
			Get("/track", _ => "");

			// { id } -> {}
			Get("/track/like", _ => "");

			// { id } -> {}
			Get("/track/dislike", _ => "");

			// { playPos, state, volume }
			Get("/player", _ => "");

			// { playPos, state, volume } -> {}
			Get("/player/update", _ => "");

			// {} -> {}
			Get("/player/next", _ => "");

		}

	}
}
