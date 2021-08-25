﻿using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentControll.Domain.Models
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
