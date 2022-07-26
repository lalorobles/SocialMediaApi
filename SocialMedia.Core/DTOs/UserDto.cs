﻿using SocialMedia.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Core.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } 
        public RoleType? Role { get; set; }
    }
}
