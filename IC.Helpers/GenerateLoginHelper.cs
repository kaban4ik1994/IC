using System.Linq;

namespace IC.Helpers
{
    public static class GenerateLoginHelper
    {
        public static string GenerateLogin(string firstName, string middleName, string lastName, int yearOfEntrance)
        {
            return string.Format("{0}_{1}{2}_{3}", lastName, firstName.ToUpper().First(), middleName.ToUpper().First(),
                yearOfEntrance % 100 >= 10 ? (yearOfEntrance % 100).ToString() : "0" + (yearOfEntrance % 100));
        }
    }
}
