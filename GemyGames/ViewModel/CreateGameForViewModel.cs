using GemyGames.Attribute;
using GemyGames.Settings;

namespace GemyGames.ViewModel
{
    public class CreateGameForViewModel
    {

        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [Display(Name ="Cateegore")]
        public int CategoreId { get; set; }

        public IEnumerable<SelectListItem>Categories=new List<SelectListItem>();

        public List<int> SelectedDevices { get; set; } = default!;

        [Display(Name ="Supported Devices")]
        public IEnumerable<SelectListItem> Devices = new List<SelectListItem>();

        [MaxLength(2500)]
        public String Discription { get; set; } = String.Empty;

        [AllowedExtension(ImageSettings.AllowedExtension)]
        //[MaxFileSize(ImageSettings.MaxSizeFileInBytes)]
        public IFormFile Cover { get; set; } = default!;

    }
}
