namespace TshirtMaker.Services
{
    public class DeepLinkService
    {
        private readonly string _websiteUrl = "https://clothiq.netlify.app";

        private readonly string _defaultShareText = "Check out my custom design on ClothIQ!";


        public string GetTwitterShareUrl(string imageUrl, string? customText = null)
        {
            var text = customText ?? _defaultShareText;
            var shareText = Uri.EscapeDataString($"{text} {_websiteUrl}");
            return $"https://twitter.com/intent/tweet?text={shareText}&url={Uri.EscapeDataString(imageUrl)}";
        }

        public string GetWhatsAppShareUrl(string imageUrl, string? customText = null)
        {
            var text = customText ?? _defaultShareText;
            var message = Uri.EscapeDataString($"{text} {imageUrl} {_websiteUrl}");
            return $"https://wa.me/?text={message}";
        }


        public string GetInstagramShareUrl(string imageUrl, string? customText = null)
        {
            return "https://www.instagram.com/";
        }

        public string GetFacebookShareUrl(string imageUrl, string? customText = null)
        {
            var text = customText ?? _defaultShareText;
            var shareUrl = Uri.EscapeDataString(_websiteUrl);
            var quote = Uri.EscapeDataString(text);
            return $"https://www.facebook.com/sharer/sharer.php?u={shareUrl}&quote={quote}&picture={Uri.EscapeDataString(imageUrl)}";
        }
    }
}
