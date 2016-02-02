﻿namespace TechSupport.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using TechSupport.Data.Common;

    public abstract class FileInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxOriginalFileNameLength)]
        public string OriginalFileName { get; set; }

        [Required]
        [MaxLength(ValidationConstants.MaxFileExtensionLength)]
        public string FileExtension { get; set; }

        public string UrlPath { get; set; }
    }
}
