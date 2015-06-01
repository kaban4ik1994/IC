using System.Collections.Generic;

namespace IC.Configuration
{
    public static class Configurations
    {
        public static string AdminEmail
        {
            get
            {
                return "Admin@gmail.com";
            }
        }

        public static string AdminPassword
        {
            get
            {
                return "Admin";
            }
        }

        public static string AdminFirstName
        {
            get
            {
                return "Ivan";
            }
        }

        public static string AdminSecondName
        {
            get
            {
                return "Ivanov";
            }
        }

        public static List<string> Roles
        {
            get
            {
                return new List<string> { "Admin", "User" };
            }
        }

        public static int QuantityOfComputers
        {
            get
            {
                return 15;
            }
        }

        public static int QuantityOfSpecialities
        {
            get
            {
                return 10;
            }
        }

        public static int QuantityOfGroups
        {
            get
            {
                return 30;
            }
        }

        public static int QuantityOfStudents
        {
            get
            {
                return 400;
            }
        }
    }
}
