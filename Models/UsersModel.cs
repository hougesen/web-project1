namespace AAOAdmin.Models
{
    public class UsersModel
    {
        public int UserId { get; set; }

        public int UserTypeId { get; set; }

        public string UserFullName { get; set; }

        public string UserPhoneNumber { get; set; }

        public string UserEmail { get; set; }

        string UserPassword { get; set; }

    }
}
