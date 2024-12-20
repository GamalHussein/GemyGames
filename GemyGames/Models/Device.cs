using System.ComponentModel.DataAnnotations;

namespace GemyGames.Models
{
    public class Device : BaseEntity
    {
        [MaxLength(50)]
        public string Icon { get; set; }
    }
}
