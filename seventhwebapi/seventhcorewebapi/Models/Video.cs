namespace SeventhCoreWebAPI.Models
{
    public class Video
    {
        public Video()
        {
        }

        public string Id { get; set; }
        public string ServerId { get; set; }
        public string? Description { get; set; }
        public int SizeInBytes { get; set; }
        public bool Recycled { get; set; }
    }
}
