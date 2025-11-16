using System.Text.Json.Serialization;
namespace ECommerceApp.Models
{
    // Enum representing available refund methods.
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RefundMethod
    {
        Original,   // Refund back to the original payment method
        PayPal,
        Stripe,
        BankTransfer,
        Manual
    }
}