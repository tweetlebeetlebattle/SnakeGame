namespace SnakeA.UserModels.Users
{
	public class UserService
	{
		private User user;
		public UserService(User _user)
		{
			user = _user;	
		}
		public void RegisterUserInDatabase()
		{
			// user.Name
			// user.Password
		}
		public List<User> GetAllUsersFromDatabase()
		{
			List<User> users = new List<User>();
			return users;
		}
	}
}
