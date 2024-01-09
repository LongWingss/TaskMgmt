namespace TaskMgmt.Api.Dtos.User
{
    interface SignUpDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }

    }
}