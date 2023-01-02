namespace ContactsAPI.Models
{
    public class AddContactRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }

        public static implicit operator string(AddContactRequest v)
        {
            throw new NotImplementedException();
        }
    }
}
