using System;
using Domain.User;

namespace Presentation
{
    public class ConsoleUI
    {
        public void Run()
        {
            Console.WriteLine("Welcome to the app.");
            while (true)
            {
                var userType = PromptForUserType();
                var user = PromptLoginOrCreate(userType);
                if (user == null)
                {
                    Console.WriteLine("Login/create cancelled. Restarting...");
                    continue;
                }

                ShowMenuFor(user);

                Console.WriteLine("Do you want to exit the application? (y/N)");
                var exitAnswer = Console.ReadLine()?.Trim().ToLowerInvariant();
                if (exitAnswer == "y" || exitAnswer == "yes")
                    break;
            }
        }

        private UserType PromptForUserType()
        {
            while (true)
            {
                Console.WriteLine("Select user type:");
                Console.WriteLine("1) Borrower");
                Console.WriteLine("2) Employee");
                Console.WriteLine("3) Admin");
                Console.Write("Choice: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
                    return (UserType)choice;

                Console.WriteLine("Invalid choice. Try again.");
            }
        }

        private User? PromptLoginOrCreate(UserType type)
        {
            while (true)
            {
                Console.WriteLine("1) Login");
                Console.WriteLine("2) Create new account");
                Console.WriteLine("0) Cancel");
                Console.Write("Choice: ");
                var input = Console.ReadLine();
                if (input == "1")
                {
                    var user = Login(type);
                    if (user != null) return user;
                    Console.WriteLine("Login failed. Try again.");
                }
                else if (input == "2")
                {
                    var user = CreateAccount(type);
                    if (user != null) return user;
                }
                else if (input == "0")
                {
                    return null;
                }
            }
        }

        private User? Login(UserType expectedType)
        {
            Console.Write("Name: ");
            var name = Console.ReadLine()?.Trim() ?? "";
            Console.Write("SSN: ");
            var ssn = Console.ReadLine()?.Trim() ?? "";

            var user = UserRepository.FindByNameAndSSN(name, ssn);
            if (user == null)
            {
                Console.WriteLine("No user found with that name and SSN.");
                return null;
            }

            if (user.Type != expectedType)
            {
                Console.WriteLine($"User type mismatch. Expected {expectedType}, but account is {user.Type}.");
                return null;
            }

            Console.WriteLine($"Welcome back, {user.Name} ({user.Type}).");
            return user;
        }

        private User? CreateAccount(UserType type)
        {
            Console.Write("Name: ");
            var name = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                return null;
            }

            Console.Write("Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age) || age < 0)
            {
                Console.WriteLine("Invalid age.");
                return null;
            }

            Console.Write("SSN: ");
            var ssn = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(ssn))
            {
                Console.WriteLine("SSN cannot be empty.");
                return null;
            }

            if (UserRepository.FindByNameAndSSN(name, ssn) != null)
            {
                Console.WriteLine("An account with that name and SSN already exists.");
                return null;
            }

            var user = new User(name, age, ssn, type);
            UserRepository.Add(user);
            Console.WriteLine($"Account created. Welcome, {user.Name} ({user.Type}).");
            return user;
        }

        private void ShowMenuFor(User user)
        {
            switch (user.Type)
            {
                case UserType.Borrower:
                    BorrowerMenu(user);
                    break;
                case UserType.Employee:
                    EmployeeMenu(user);
                    break;
                case UserType.Admin:
                    AdminMenu(user);
                    break;
            }
        }

        private void BorrowerMenu(User user)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Borrower Menu:");
                Console.WriteLine("1) View media");
                Console.WriteLine("0) Logout");
                Console.Write("Choice: ");
                var choice = Console.ReadLine();
                if (choice == "1")
                    user.ViewMedia();
                else if (choice == "0")
                    break;
                else
                    Console.WriteLine("Invalid choice.");
            }
        }

        private void EmployeeMenu(User user)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Employee Menu:");
                Console.WriteLine("1) View media");
                Console.WriteLine("2) Add media");
                Console.WriteLine("3) Remove media");
                Console.WriteLine("0) Logout");
                Console.Write("Choice: ");
                var choice = Console.ReadLine();
                if (choice == "1")
                    user.ViewMedia();
                else if (choice == "2")
                {
                    Console.Write("Title to add: ");
                    var title = Console.ReadLine() ?? "";
                    user.AddMedia(title);
                }
                else if (choice == "3")
                {
                    Console.Write("Title to remove: ");
                    var title = Console.ReadLine() ?? "";
                    user.RemoveMedia(title);
                }
                else if (choice == "0")
                    break;
                else
                    Console.WriteLine("Invalid choice.");
            }
        }

        private void AdminMenu(User user)
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Admin Menu:");
                Console.WriteLine("1) List users");
                Console.WriteLine("2) Remove user");
                Console.WriteLine("3) Create user");
                Console.WriteLine("0) Logout");
                Console.Write("Choice: ");
                var choice = Console.ReadLine();
                if (choice == "1")
                {
                    Console.WriteLine("Registered users:");
                    foreach (var u in UserRepository.GetAll())
                    {
                        Console.WriteLine($"- {u.Name} | Age: {u.Age} | SSN: {u.SSN} | Type: {u.Type}");
                    }
                }
                else if (choice == "2")
                {
                    Console.Write("Name of user to remove: ");
                    var name = Console.ReadLine() ?? "";
                    Console.Write("SSN of user to remove: ");
                    var ssn = Console.ReadLine() ?? "";
                    var target = UserRepository.FindByNameAndSSN(name, ssn);
                    if (target == null)
                        Console.WriteLine("User not found.");
                    else if (UserRepository.Remove(target))
                        Console.WriteLine("User removed.");
                    else
                        Console.WriteLine("Failed to remove user.");
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Create a new user (admin shortcut):");
                    var newUser = CreateAccount(PromptUserTypeForCreation());
                    if (newUser != null)
                        Console.WriteLine("User created.");
                }
                else if (choice == "0")
                    break;
                else
                    Console.WriteLine("Invalid choice.");
            }
        }

        private UserType PromptUserTypeForCreation()
        {
            while (true)
            {
                Console.WriteLine("Select type for the new user:");
                Console.WriteLine("1) Borrower");
                Console.WriteLine("2) Employee");
                Console.WriteLine("3) Admin");
                Console.Write("Choice: ");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= 3)
                    return (UserType)choice;
                Console.WriteLine("Invalid choice.");
            }
        }
    }
}