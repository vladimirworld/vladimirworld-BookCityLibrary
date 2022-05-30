﻿using System.ComponentModel.DataAnnotations;

namespace BookCityLibrary.UI.Models;

public class UserModel
{
    [Required]
    public string Username { get; set; }
        
    [Required]
    public string Password { get; set; }
}