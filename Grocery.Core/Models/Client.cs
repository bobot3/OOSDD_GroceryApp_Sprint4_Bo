﻿
namespace Grocery.Core.Models
{
    public enum Role
    {
        User,
        Admin
    }
    public partial class Client : Model
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public Client(int id, string name, string emailAddress, string password, Role role) : base(id, name)
        {
            EmailAddress=emailAddress;
            Password=password;
            Role=role;
        }
    }
}
