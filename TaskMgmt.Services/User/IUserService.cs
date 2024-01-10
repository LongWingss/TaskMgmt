namespace TaskMgmt.Services
{
    public interface IUserService
    {
        public string EncryptPassword(string password);
        public bool VerifyPassword(string enteredPassword, string storedPasswordHash);
        public Task<string> Authenticate(string email, string enteredPassword);
        public Task<string> SignUp(string email, string enteredPassword, string name, string groupName);
        public Task<string> SignUpWithReferral(string email, string enteredPassword, string name, string referralCode, string groupName);

        public Task<bool> IsUserInGroup(int userId, int groupId);
    }
}