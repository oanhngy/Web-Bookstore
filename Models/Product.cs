using System.Collections.Generic;
using System.ComponentModel;
namespace BookstoreWeb.Models
{
    public class Product
    {
        public int ProductID {get; set;} //PK
        public string Name {get; set;}
        public string? Description {get; set;} //?=nullable
        public string? Author {get; set;}
        public decimal Price {get; set;}
        public int CategoryID {get; set;} //FK to Category
        public Category? Category {get; set;} //navigation property=lấy thông tin danh mục đầy đủ, dùng trong Include(p=>p.Category)
        public List<ProductImage>? ProductImages {get; set;}
    }
}