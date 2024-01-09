using System.ComponentModel.DataAnnotations.Schema;

namespace SeventhCoreWebAPI.Models
{
    public class Video
    {
        public string Id { get; set; }
        public string ServerId { get; set; }
        public string? Description { get; set; }
        public long SizeInBytes { get; set; }
        public bool Recycled { get; set; }

        [NotMapped]
        public string Base64Binary { get; set; }
    }
}
