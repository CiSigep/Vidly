using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.DTO
{
    public class MoviesDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Range(1, 20)]
        public byte NumberInStock { get; set; }

        [Required]
        public DateTime DateReleased { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        [Required]
        public int GenreId { get; set; }

        public GenreDTO Genre { get; set; }
    }
}