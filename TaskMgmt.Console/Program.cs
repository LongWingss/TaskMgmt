using TaskMgmt.Console;
static class Program
{
    public static void Main()
    {
        Console.WriteLine("Welcome to the Task Management Console App!");
        Menu menu = new Menu();
        while (true)
        {
            Console.WriteLine("\n1. Sign In");
            Console.WriteLine("2. Sign Up");
            Console.WriteLine("3. Sign Up with Referral");
            Console.WriteLine("4. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var signInTask = menu.SignIn();
                    if(signInTask.Result)
                    {
                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("Sign-in failed. Please try again.");
                    }
                    break;
                case "2":
                    menu.SignUp();
                    break;
                case "3":
                    menu.SignUpReferral();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }





}
