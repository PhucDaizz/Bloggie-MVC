﻿namespace Bloggie.web.Models.ViewModels
{
    public class UserViewModel
    {
        public List<User> Users { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool AdminRoleCheckBox { get; set; }
    }
}
