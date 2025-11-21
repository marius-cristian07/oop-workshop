namespace Domain.User
{
    public enum UserType
    {
        Borrower = 1,
        Employee = 2,
        Admin = 3
    }

    public class User
    {
        public string Name { get; init; }
        public int Age { get; init; }
        public string SSN { get; init; }
        public UserType Type { get; init; }

        public User(string name, int age, string ssn, UserType type)
        {
            Name = name;
            Age = age;
            SSN = ssn;
            Type = type;
        }

        // Borrower ability
        public void ViewMedia()
        {
            System.Console.WriteLine($"[User: {Name}] Viewing available media...");
            // placeholder - integrate real media store here
        }

        // Employee abilities
        public void AddMedia(string title)
        {
            System.Console.WriteLine($"[Employee: {Name}] Adding media: {title}");
            // placeholder - integrate real media store here
        }

        public void RemoveMedia(string title)
        {
            System.Console.WriteLine($"[Employee: {Name}] Removing media: {title}");
            // placeholder - integrate real media store here
        }

        // Admin abilities
        public void ManageUsers()
        {
            System.Console.WriteLine($"[Admin: {Name}] Opening user management...");
            // placeholder - admin-specific actions can be expanded
        }
    }

    public static class UserRepository
    {
        private static readonly List<User> _users = new();

        public static IEnumerable<User> GetAll() => _users;

        public static void Add(User user)
        {
            _users.Add(user);
        }

        public static User? FindByNameAndSSN(string name, string ssn)
        {
            return _users.FirstOrDefault(u => u.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase) && u.SSN == ssn);
        }

        public static bool Remove(User user)
        {
            return _users.Remove(user);
        }
    }
}