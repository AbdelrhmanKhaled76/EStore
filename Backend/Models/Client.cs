using EStore.Controllers.ResponseModels;

namespace EStore.Models;

public class Client
{
    public Client()
    {
        
    }
    public Client(ShippingData data)
    {
        FirstName = data.FirstName;
        LastName = data.LastName;
        Street = data.Street;
        PhoneNumber = data.PhoneNumber;
        Email = data.Email;
    }
    public int Id { get; set; }
    public  string FirstName { get; set; }

    public  string LastName { get; set; }

    public  string Street { get; set; }

    public  string PhoneNumber { get; set; }

    public string Email { get; set; }

    public Order Order { get; set; } = null!;

    public override string ToString()
    {
        return $"FullName: {FirstName + " " + LastName}\nPhone Number: {PhoneNumber}\nEmail: {Email}\nAddress: {Street}\n";
    }

}
