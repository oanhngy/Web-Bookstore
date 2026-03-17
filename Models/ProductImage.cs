namespace BookstoreWeb.Models
{
    public class ProductImage
    {
        public int ImageID {get; set;} //PK
        public int ProductID {get; set;} //FK to Product
        public string ImagePath {get; set;} //ảnh lưu ở wwwroot/images
        public bool IsPrimary {get; set;}
        public string ImageType {get; set;} //not neccesary nhưng codebase có dùng -> refactor sau
        public ProductImage? Product {get; set;}
    }
}