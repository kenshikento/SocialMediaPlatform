using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMediaPlatform.Models
{
    public class SocialMedia
    {
        [Required]
        public string id { get; set; }
        [Required]
        public Platform Type { get; set; }
    }

    public enum Platform
    {
        Youtube,
        Spotify
    }
}