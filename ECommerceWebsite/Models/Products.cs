using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWebsite.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Microsoft.Build.Framework.Required]
        public string ?Name { get; set; }
        [Microsoft.Build.Framework.Required]
        public decimal Price { get; set; }  

        public string ?Image { get; set; }
        [Display(Name="Product Color")]
        
        public string ?ProductColor { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name ="Available")]
        public bool IsAvailable { get; set; }


        [Display(Name ="Product Category")]
        [Microsoft.Build.Framework.Required]
        public int ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductTypes? ProductTypes { get; set; }

        [Display(Name = "Special tag")]
        [System.ComponentModel.DataAnnotations.Required]  
        public int SpecialTagId { get; set; }
        [ForeignKey("SpecialTagId")]
        public TagNames ?TagNames { get; set; }
    }
}
