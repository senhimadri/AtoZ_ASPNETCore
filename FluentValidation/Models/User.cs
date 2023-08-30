namespace FluentValidation.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Membership { get; set; }
    }
}
