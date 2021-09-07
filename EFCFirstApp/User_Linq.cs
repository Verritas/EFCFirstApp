namespace EFCFirstApp
{
    public class User_Linq
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public int CompanyId { get; set; }
        public Company_Linq Company { get; set; }
    }
}
