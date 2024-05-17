using SnakeA.UserModels.Users.Interface;

namespace SnakeA.UserModels.Users
{
	public class User : IUser
	{
        private string name;
        private string password;
		private string profilePic;
        public User(string _name, string password)
		{
			name = _name;
			this.password = password;
			profilePic = "null";
		}
		public string Name
        {
            get { return name; }
        }
		public string Password
		{
			get { return password; }
		}
		public string ProfilePic 
		{ get
			{
				return profilePic;
			} 
		}
	}
}
