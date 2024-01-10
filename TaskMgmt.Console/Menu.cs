using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaskMgmt.Console.Dtos.User;
using TaskMgmt.Console.Dtos.Group;
using System.Text.RegularExpressions;
using System.ComponentModel;
using TaskMgmt.Console.Dtos;
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
        public async Task<bool> SignIn()
        {
            System.Console.Write("Enter Email: ");
            string email = System.Console.ReadLine();
            System.Console.Write("Enter Password: ");
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
                var response = await apiClient.PostAsync("login", loginDtoConsole);
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
                Home(userToken);
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
            try
            {
                var response = await apiClient.PostAsync("signup", signUpDtoConsole);
                if(response.IsSuccessStatusCode)
                {
                    userToken = await response.Content.ReadAsStringAsync();
                    System.Console.WriteLine("Welcome.");
                    Home(userToken);
                }
                else
                {
                    System.Console.WriteLine("SignUp Failed.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
        public async void SignUpReferral()
        {
            System.Console.Write("Enter Name: ");
            string name = System.Console.ReadLine();
            System.Console.Write("Enter Email: ");
            string email = System.Console.ReadLine();
            System.Console.Write("Enter Password");
            string password = System.Console.ReadLine();
            System.Console.Write("Enter Groupname");
            string groupName = System.Console.ReadLine();
            System.Console.Write("Enter Referral: ");
            string referral = System.Console.ReadLine();
            //call signureferral api
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
            var signUpReferralDtoConsole = new SignUpReferralDTO
            {
                Email = email,
                Name = name,
                Password = password,
                GroupName = groupName,
                ReferralCode = referral
            };
            try
            {
                var response = await apiClient.PostAsync("signup", signUpReferralDtoConsole);
                if (response.IsSuccessStatusCode)
                {
                    userToken = await response.Content.ReadAsStringAsync();
                    System.Console.WriteLine("Welcome.");
                    Home(userToken);
                }
                else
                {
                    System.Console.WriteLine("SignUp Failed.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
        //function called when user successfully signup or login
        public async void Home(string userToken)
        {
            System.Console.Clear();
            System.Console.WriteLine("\t\t\t\t\t\tHOME DASHBOARD");
            try
            {
                var responseTask =  apiClient.GetAsyncToken("groups", userToken);
                var response = responseTask.Result;
                if(response.IsSuccessStatusCode)
                {
                    System.Console.WriteLine("\t\t\tGroup Details");
                    string content = await response.Content.ReadAsStringAsync();
                    var groups = JsonSerializer.Deserialize<List<GroupDTO>>(content);
                    foreach(var group in groups)
                    {
                        System.Console.WriteLine($"GroupID: {group.groupId} GroupName: {group.groupName} CreatedAt: {group.createdAt}\n");
                    }
                    System.Console.WriteLine("\n\n");
                    HomeMenu(userToken);
                }
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }
        public void HomeMenu(string userToken)
        {
            while(true)
            {
                System.Console.WriteLine("1.Select Group (Id) \n2.Enroll to Group\n3.Create Group\n\n");
                System.Console.Write("Choose an option: ");
                string choice = System.Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        //select groupID
                        System.Console.Write("Enter Group ID");
                        int groupID = System.Convert.ToInt32(System.Console.ReadLine());
                        var flag = GetProjects(groupID, userToken);
                        //while (flag.Result);
                        break;
                    case "2":
                        //enroll
                        break;
                    case "3":
                        //create
                        System.Console.Write("Enter Group Name : ");
                        var GroupName = System.Console.ReadLine();
                        CreateGroup(GroupName, userToken);
                        
                        break;
                    default:
                        System.Console.WriteLine("Invalid option");
                        break;
                }
                System.Console.WriteLine("Press 0 to exit");
                int ans = Convert.ToInt32(System.Console.ReadLine());
                if(ans == 0)
                {
                    break;
                }
                else
                {
                    System.Console.Clear();
                }
            }
        }

        public async Task CreateGroup(string Name,string Token)
        {
            GroupRequestDTO GroupRequestDTO = new GroupRequestDTO
            {
                GroupName = Name
            };
            try
            {
                var response = await apiClient.PostAsyncToken("groups", GroupRequestDTO, Token);
                if (response.IsSuccessStatusCode)
                {
                    System.Console.WriteLine("Group created successfully\n");
                    HomeMenu(Token);
                }
                else
                {
                    System.Console.WriteLine("Group Creation Failed.");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

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

