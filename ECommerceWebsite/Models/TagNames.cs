using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ECommerceWebsite.Models
{
    public class TagNames
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tag Name")]
        public string TagName { get; set; }
    }
}
