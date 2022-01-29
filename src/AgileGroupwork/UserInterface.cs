namespace MemeTeamPro
{
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
    public class UserInterface
    {
        public UserInterface()
        {}
        public void Start()
        {
         string input;
            string savePath = (@"\src\AgileGroupwork\");
            int ID = 0;
            bool login = false;

            string[] usernameArray = File.ReadAllLines(@"username.txt");//loads a text file and sets it to an array
            ArrayList username = new ArrayList(usernameArray);//sets the array to an array list
            string[] passwordArray = File.ReadAllLines(@"password.txt");
            ArrayList password = new ArrayList(passwordArray);
            string[] timeArray = File.ReadAllLines(@"time.txt");
            ArrayList time = new ArrayList(timeArray);

        start:
            if (login == true)
            {
                goto menu;
            }
            Console.Clear();
            Console.WriteLine(@"What would you like to do?
1 Login
2 Register
3 Shut Down");
            input = Console.ReadLine();

            switch(input)
                {
                case "1":
                case "login":
                    Console.WriteLine("What is your user name?");
                    input = Console.ReadLine();
                    input = input.ToLower();
                    if (input =="default")
                    {
                        Console.WriteLine("Please try another user name");
                        Console.ReadKey();
                        goto start;
                    }
                    foreach (string name in username)//runs through the username list
                    {
                        if (name == input)//returns true if it finds a match in the list
                        {
                            int listNo = username.IndexOf(input);//sets the listNo to the index number of the password list that matched
                            Console.WriteLine("What is your password?");
                            input = Console.ReadLine();
                            string passCheck = Convert.ToString(password[listNo]);//sets the passCheck var to the string index no found at the same index as the user name
                            if (input == passCheck) //if the input and the passCheck are the same you logged in
                                {
                                ID = listNo;//sets the ID for the user
                                string lastLogin = Convert.ToString(time[ID]);//gets the last login from the time list
                                        Console.WriteLine(@"You logged in!
You last logged in at "+lastLogin );
                                time[ID] = (Convert.ToString(DateTime.Now));//sets a new login time
                                using (TextWriter writer = File.CreateText(@"time.txt"))//creates a txt file called time
                                  {
                                  foreach (string date in time)
                                    {
                                    writer.WriteLine(date);//adds a new line to the txt file for time
                                    }
                                  }
                                   Console.ReadKey();
                                login = true;
                                   goto start;
                                }
                        }
                    }
                   Console.WriteLine("Sorry there was some error!");
                    Console.ReadKey();
                    goto start;

                case "2":
                case "register":

                    Console.WriteLine("What would you like your user name to be?");
                    username:
                    input = Console.ReadLine();
                    input = input.ToLower();
                    if (input == "")
                    {
                        Console.WriteLine("Please input a username");
                        goto username;
                    }
                    foreach(string name in username)
                    {
                        if(name == input)//checks if there is a user name called that already
                        {
                            Console.WriteLine("Sorry this username is taken");
                            Console.ReadKey();
                            goto start;
                        }
                    }
                    username.Add(input);//adds the username to the username list
                    Console.WriteLine("What would you like your password to be?");
                    password:
                    input = Console.ReadLine();
                    if (input == "")
                    {
                        Console.WriteLine("Please enter a password");
                        goto password;
                    }
                    password.Add(input);//adds the password to the password list
                    using (TextWriter writer = File.CreateText(@"username.txt"))//creates a txt file called username
                    {
                        foreach (string name in username)
                        {
                            writer.WriteLine(name);//adds a new line to the txt file for the user
                        }
                    }
                    using (TextWriter writer = File.CreateText(@"password.txt"))
                    {
                        foreach (string pass in password)
                        {
                            writer.WriteLine(pass);
                        }
                    }
                    time.Add(Convert.ToString(DateTime.Now));
                    using (TextWriter writer = File.CreateText(@"time.txt"))//creates a txt file called username
                    {
                        foreach (string date in time)
                        {
                            writer.WriteLine(date);//adds a new line to the txt file for the user
                        }
                    }
                    Console.WriteLine("User created!");
                    Console.ReadKey();
                    break;

                case "3":
                case "shutdown":
                    Console.Clear();
                    Console.WriteLine("Shutting down");
                    Console.ReadKey();
                    Environment.Exit(0);//closes down the console
                    break;

                default:
                    Console.WriteLine("Unexpected input");
                    Console.ReadKey();
                    break;
            }
            goto start;

        menu:
            Console.Clear();

            string user = Convert.ToString(username[ID]);
            Console.WriteLine(@"Main menu 
Welcome back " + user);
            Console.WriteLine(@"
1 Add a score for a person
2 Show a person's score
3 Show all scores
4 logout
5 Shutdown
" );
            input = Console.ReadLine();
            input.ToLower();
            switch(input)
            {
                case "1":
                case "Addscore":
                    Console.WriteLine("Person's name");
                    string name = Console.ReadLine();
                    Console.WriteLine("Score");
                    int score = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Type of score");
                    string type = Console.ReadLine();
                    Console.WriteLine("Thank you! Added " + name + "'s " + score + " scores to the " + type + " scorelist!");
                    break;

                case "2":
                case "ShowPersonScrore":
                    Console.WriteLine("Which person's score would you like to see");
                    string person = Console.ReadLine();
                    Console.WriteLine("Here are all the scores for " + person + ":");
                    break;

                case "3":
                case "ShowAllScore":
                    Console.WriteLine("Here are all the scores:");
                    break;

                case "4":
                case "logout":
                    Console.WriteLine("Would you like to logout? y/n");
                    input = Console.ReadLine();
                    if (input == "y")
                    {
                        login = false;
                        ID = 0;
                        Console.WriteLine("Logged out");
                        Console.ReadKey();
                    }
                    break;

                case "5":
                case "shutdown":
                    Console.Clear();
                    Console.WriteLine("Shutting down");
                    Console.ReadKey();
                    Environment.Exit(0);//closes down the console
                    break;

                default:
                    Console.WriteLine("Unexpected input");
                    Console.ReadKey();
                    break;
            }
            goto start;
        }
    }
}