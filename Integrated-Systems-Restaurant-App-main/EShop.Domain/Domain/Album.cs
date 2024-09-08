using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERestaurant.Domain.Domain
{
    public class Album :BaseEntity
    {
        public required string Title { get; set; }
        public required string CoverImageUrl { get; set; }
        public required string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public required string Tags { get; set; }
        public required AlbumType Type { get; set; } // Enum for album type


        // One-to-many relationship with Track
        public List<Track> Tracks { get; set; } = new List<Track>();

        // Many-to-many relationship with Artist (Handled via a junction table)
        public List<Artist> Artists { get; set; } = new List<Artist>();

    }

    public enum AlbumType
    {
        EP, // Extended Play
        LP // Long Play
    }
}

