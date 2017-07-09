using System.Collections.Generic;

namespace HelloWorldRansom.ViewModels
{
    public class ImagesViewModel
    {
        public IEnumerable<string> Images { get; set; }

        public ImagesViewModel(IEnumerable<string> images, string language)
        {
            Images = images;
            Language = language;
            Languages = new Dictionary<string, string>
            {
                { "en", "English" },
                { "hr", "Croatian" },
                { "da", "Danish" },
                { "nl", "Dutch" },
                { "et", "Estonian" },
                { "fj", "Fijian" },
                { "fi", "Finnish" },
                { "fr", "French" },
                { "de", "German" },
                { "ht", "Haitian Creole" },
                { "mww", "Hmong Daw" },
                { "id", "Indonisian" },
                { "it", "Italian" },
                { "sw", "Kiswahili" },
                { "tlh", "Klingon" },
                { "lv", "Latvian" },
                { "lt", "Lithuanian" },
                { "mg", "Malagasy" },
                { "ms", "Malay" },
                { "yua", "Yucatec Maya" },
                { "no", "Norwegian Bokmål" },
                { "otq", "Querétaro Otomi" },
                { "sm", "Samoan" },
                { "sr-Latn", "Serbian (Latin)" },
                { "sl", "Slovenian" },
                { "es", "Spanish" },
                { "ty", "Tahitian" },
                { "to", "Tongan" },
                { "cy", "Welsh" }
            };
        }

        public string Language { get; set; }

        public Dictionary<string, string> Languages { get; set; }
    }
}
