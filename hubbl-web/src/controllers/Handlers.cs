using hubbl.web.models;
using Nancy.ModelBinding;

namespace hubbl.web {
	
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
			Get("/signin", parameters => User.toLoginResponse(User.tryLogin(Request.Query["login"], Request.Query["password"])));

			// { name, login, password } -> {}
			Get("/signup", parameters => User.toSignUpResponse(User.trySignUp(Request.Query["name"], Request.Query["login"], Request.Query["password"])));

		}

		private void initAwailableForAutentificated() {

			// {} -> { hubs: [ { id, name, author }, ... ] }
			Get("/hubs", parameters => {
				return "";
			});

			// { query } -> { hubs: [ { id, name, author }, ... ] }
			Get("/hubs/search", parameters => {
				return "";
			});

			// { id } -> { name, author }
			Get("/hub", parameters => {
				return "";
			});

			// { id } -> {}
			Get("/hub/connect", parameters => {
				return "";
			});

			// { name } -> { id }
			Get("/hub/new", parameters => {
				return "";
			});

		}

		private void initAwailableForConnected() {

			// {} -> {}
			Get("/hub/delete", parameters => {
				return "";
			});

			// { id } -> { users: [ { id, name }, ... ] }
			Get("/hub/users", parameters => {
				return "";
			});

			// { id } -> { name }
			Get("/user", parameters => {
				return "";
			});

			// {} -> { playlist: [ ... ] }
			Get("/playlist", parameters => {
				return "";
			});

			// {} -> { playlist: [ ... ] }
			Get("/playlist/full", parameters => {
				return "";
			});

			// { id } -> { ... }
			Get("/track", parameters => {
				return "";
			});

			// {} -> { playlist: [ ... ] }
			Get("/track/like", parameters => {
				return "";
			});

			// { id } -> {}
			Get("/track/dislike", parameters => {
				return "";
			});

			// { playPos, state, volume }
			Get("/player", parameters => {
				return "";
			});

			// { playPos, state, volume } -> {}
			Get("/player/update", parameters => {
				return "";
			});

			// {} -> {}
			Get("/player/next", parameters => {
				return "";
			});

		}

	}
}
