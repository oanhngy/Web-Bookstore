using System;
using System.Collections.Generic;
namespace BookstoreWeb.Models
{
    public class Order
    {
        public int OrderID {get; set;} //PK
        public string UserID {get; set;} //FK to AspNetUsers và dùng string vì bảng đó dùng
        public string Status {get; set;} //New, Checked Outm Confirmed, Completed/Cancelled
        public DateTime? OrderDate {get; set;}
        public decimal? TotalAmount {get; set;}

        //ConfirmOrder field
        public string? FullName {get; set;}
        public string? Email {get; set;}
        public string? Phone {get; set;}
        public string? Address {get; set;}
        public string? Note {get; set;}
        public string? PaymentMethod {get; set;}
        public List<OrderDetail>? OrderDetails {get; set;}
    }
}