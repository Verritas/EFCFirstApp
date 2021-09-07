using System.Collections.Generic;

namespace EFCFirstApp
{
    public class Company_Linq
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<User_Linq> Users {get; set; } = new List<User_Linq>();
    }
}
