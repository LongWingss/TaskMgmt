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

        public HttpClient _httpClient = new HttpClient();

        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://localhost:7197/api"),
        };

        //new code here
        private readonly ApiClient apiClient;

        public Menu()
        {
            apiClient = new ApiClient(ApiUrl);
        }

        //

        public async void SignIn()
        {
            var result = await apiClient.GetAsync<List<GroupDTO>>("groups");
            foreach(var i in result)
            {
                System.Console.WriteLine($"GroupName: {i.GroupName}\n");
            }
        }
        //Function called when Option Signup is choosen.
        //public async void SignIn()
        //{
        //    System.Console.Write("Enter Email: ");
        //    string email = System.Console.ReadLine();
        //    System.Console.Write("Enter Password");
        //    string password = System.Console.ReadLine();
        //    //call signin rest api
        //    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        //    {
        //        System.Console.WriteLine("Please provide both email and password.");
        //        return;
        //    }
        //    if (!IsValidEmail(email))
        //    {
        //        System.Console.WriteLine("Invalid email format. Please enter a valid email address.");
        //        return;
        //    }

        //    if (!IsStrongPassword(password))
        //    {
        //        System.Console.WriteLine("Weak password. Please use a stronger password.");
        //        return;
        //    }
        //    var loginDtoConsole = new LoginDTO
        //    {
        //        Email = email,
        //        Password = password
        //    };
        //    HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{ApiUrl}/login", JsonSerializer.Serialize(loginDtoConsole));
        //    if (response.IsSuccessStatusCode)
        //    {
        //        System.Console.Write($"Welcome, {email}");
        //        //next phase , call Home()
        //    }
        //    else
        //    {
        //        System.Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
        //    }
        //}

        //Function called when Signup option choosen.
        public async void SignUp()
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
            var request = new HttpRequestMessage(HttpMethod.Post, $"{ApiUrl}/signup");
            string jsonContent = JsonSerializer.Serialize(signUpDtoConsole);
            StringContent stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            request.Content = stringContent;
            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                System.Console.Write($"Welcome, {email}");
            }
            else
            {
                System.Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
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


