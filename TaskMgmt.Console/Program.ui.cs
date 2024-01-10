// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using TaskMgmt.Console;

class Program2
{
    static List<string>? ConsoleReadInput()
    {
        string? input = Console.ReadLine();
        if (input == null || input.Length == 0 || input == " ")
            return null;

        //Accounting for quotes
        List<string> output = new List<string>();

        string[] args = input.Split('\'', '"').ToArray();
        foreach (var arg in args)
        {
            if (arg == "" || arg == " ") continue;

            arg.Split(' ').ToList().ForEach(e =>
            {
                if (e != "")
                    output.Add(e);
            });
        }
        return output;
    }

    const string COMMAND_OPTIONS =
    @$"
        COMMANDS
        --------
            /login              := user login 
            /signup             := user signup 
            /createproject      := create a project
        
    ";
    static void Main()
    {
        const string API_URL_BASEADDRESS = "https://localhost:7197/";
        ApiClient client = new ApiClient(API_URL_BASEADDRESS);

        ConsoleProject cproj = new ConsoleProject(client);

        Menu menu = new Menu(client);

        bool isRunning = true;
        while (isRunning)
        {
            Console.Write("> ");
            List<string> args = ConsoleReadInput();
            if (args == null) continue;

            switch (args[0].ToLower())
            {
                case "/login":
                    var resp = menu.SignIn();
                    if (resp.Result)
                    {
                        Console.WriteLine("Success");
                    }
                    else
                    {
                        Console.WriteLine("Sign-in failed. Please try again.");
                    }
                    break;

                case "/signup":
                    menu.SignUp();
                break;

                case "/createproject":

                    Console.Write("Project name: ");
                    string projName = Console.ReadLine();

                    Console.Write("Project description: ");
                    string projDes = Console.ReadLine();

                    Console.Write("Group id: ");
                    if (!int.TryParse(Console.ReadLine(), out int groupId))
                    {
                        Console.WriteLine("groupId should be a number, passed 3rd argument is a string");
                        continue;
                    }

                    cproj.CreateNewProject(projName, projDes, groupId);
                    break;
                case "/help":
                    System.Console.WriteLine(COMMAND_OPTIONS);
                    break;
                case "/exit":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid command!");
                    break;
            }
        }
    }
}

