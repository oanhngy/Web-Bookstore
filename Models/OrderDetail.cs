namespace BookstoreWeb.Models
{
    public class OrderDetail
    {
        public int OrderDetailID {get; set;} //PK
        public int OrderID {get; set;} //FK to Order
        public int ProductID {get; set;} //FK to Product
        public int Quantity {get; set;}
        public decimal UnitPrice {get; set;} //giá sp tại thời điểm mua, không dùng Product.Price vì giá có thể thay đổi, tránh đổi giá trong đơn cũ khi sửa giá mới

        public Order? Order {get; set;}
        public Product? Product{get; set;}
    }
}