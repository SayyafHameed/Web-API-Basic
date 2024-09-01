namespace WebAPIBasic
{
    public class AttachmentsOptions
    {
            public string AllowedExtention { get; set; }
            public int MaxSizeInMegaBytes { get; set; }
            public bool EnableCompression { get; set; }
    }
}
