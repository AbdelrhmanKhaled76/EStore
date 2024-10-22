using EStore.Controllers.ResponseModels;
using EStore.Models.Enums;

namespace EStore.Models;

public class PaymobTransaction
{
    public PaymobTransaction()
    {
        
    }

    public PaymobTransaction(TransactionData data)
    {
        Id = data.TransactionId;
        Status = data.Success ? "Success" : data.Pending ? "Pending" : "Error";
        TotalPrice = data.AmountCents / 100m;
        SubType = data.SourceData.SubType;
        CreatedAt = data.CreatedAt;
        UpdatedAt = data.UpdatedAt;
        ErrorOccurred = data.ErrorOccurred;
        IsLive = data.IsLive;
        SourceId = data.SourceId;
    }

    public long Id { get; set; }

    public string Status { get; set; } = null!;
    public decimal TotalPrice { get; set; }

    public string SubType { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public bool ErrorOccurred { get; set; }

    public bool IsLive { get; set; }

    public long SourceId { get; set; }

    public Order Order { get; set; } = null!;

}