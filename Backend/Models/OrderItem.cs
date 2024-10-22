

using EStore.Models.DTOs;

namespace EStore.Models;

public class OrderItem
{

    public OrderItem()
    {
        
    }

    public OrderItem(OrderItemDTO orderItem)
    {
        Price = orderItem.Price;
        Quantity = orderItem.Quantity;
        ProductId = orderItem.ProductId;        
    }
    public int Id { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }


    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;

    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public override string ToString()
    {
        return $"ProductId: {ProductId}, Quantity: {Quantity}, Name: {Product.Title}";
    }
}