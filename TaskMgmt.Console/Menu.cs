using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskMgmt.Console.Dtos.User;
using TaskMgmt.Console.Dtos.Group;

namespace TaskMgmt.Console
{
    public class Menu
    {
        private readonly string ApiUrl = "https://localhost:7197/api/";
        private string userToken;
        private readonly ApiClient apiClient;

        public Menu()
        {
            apiClient = new ApiClient(ApiUrl);
        }

        //

        //public async void SignIn()
        //{
        //    var result = await apiClient.GetAsync<List<GroupDTO>>("groups");
        //    foreach (var i in result)
        //    {
        //        System.Console.WriteLine($"GroupName: {i.GroupName}\n");
        //    }
        //}
        //Function called when Option Signup is choosen.
        public async Task<bool> SignIn()
        {
            System.Console.Write("Enter Email: ");
            string email = System.Console.ReadLine();
            System.Console.Write("Enter Password");
            string password = System.Console.ReadLine();
            //call signin rest api
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                System.Console.WriteLine("Please provide both email and password.");
                return false; 
            }
            if (!IsValidEmail(email))
            {
                System.Console.WriteLine("Invalid email format. Please enter a valid email address.");
                return false;
            }

            if (!IsStrongPassword(password))
            {
                System.Console.WriteLine("Weak password. Please use a stronger password.");
                return false;
            }
            var loginDtoConsole = new LoginDTO
            {
                Email = email,
                Password = password
            };
            try
            {
                var response = await apiClient.PostAsync<LoginDTO>("login", loginDtoConsole);
                if(response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    userToken = content;
                }
                else
                {
                    System.Console.WriteLine("Login Failed");
                    return false;
                }
                Home();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            return true;
        }

        //Function called when Signup option choosen.
        public async Task SignUp()
        {
            System.Console.Write("Enter Name: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Enter Email: ");
            string email = System.Console.ReadLine();
            System.Console.Write("Enter Password");
            string password = System.Console.ReadLine();
            System.Console.Write("Enter Groupname");
            string groupName = System.Console.ReadLine();
            //call signup restapi
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                System.Console.WriteLine("Please provide both email and password.");
                return;
            }
            if (!IsValidEmail(email))
            {
                System.Console.WriteLine("Invalid email format. Please enter a valid email address.");
                return;
            }

            if (!IsStrongPassword(password))
            {
                System.Console.WriteLine("Weak password. Please use a stronger password.");
                return;
            }
            var signUpDtoConsole = new SignUpDTO
            {
                Email = email,
                Name = name,
                Password = password,
                GroupName = groupName
            };
            var response = await apiClient.PostAsync<SignUpDTO>("signup", signUpDtoConsole);
            System.Console.WriteLine(response.ToString());
        }
        public void SignUpReferral()
        {
            System.Console.Write("Enter Name: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Enter Email: ");
            string email = System.Console.ReadLine();
            System.Console.Write("Enter Password");
            string password = System.Console.ReadLine();
            System.Console.Write("Enter Referral: ");
            string referral = System.Console.ReadLine();
            //call signureferral api
        }
        //function called when user successfully signup or login
        public void Home()
        {
            System.Console.Clear();

        }

        //Function for checking validitiy of email
        public bool IsValidEmail(string email)
        {
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(email);
        }
        //function for checking strength of password.
        public bool IsStrongPassword(string password)
        {
            return password.Length > 8;
        }
    }
}

