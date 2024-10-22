using EStore.Controllers.RequestModels;
using EStore.Controllers.ResponseModels;
using EStore.Models.Enums;
using System.Text.Json.Serialization;

namespace EStore.Models;

public class Order
{

    public int Id { get; set; }
    public long? PaymobOrderId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public required string Status { get; set; }

    public decimal TotalPrice { get; set; }
    public decimal RemainingCash { get; set; }

    public long? TransactionId { get; set; }
    public PaymobTransaction? Transaction { get; set; } = null!;


    public int? ClientId { get; set; }
    public  Client? Client { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


}
