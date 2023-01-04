
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieProject.Repository.Entities
{
    public class MovieModel
    {
        //ตัวแปรข้อมูลใน database [Required] = ต้องการค่า 
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public string coverImg { get; set; }
        [Required]
        public string? releaseDate { get; set; }
        [Required]
        public string genre { get; set; }
        public int duration { get; set; }
        public string? createDate { get; set; }
        public string? modifyDate { get; set; }
    }
}