using System.Text.Json.Serialization;

namespace to_due.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ExpenseClass
    {
        None = 1,        // No recurrence
        Weekly = 2,      // Weekly recurrence
        BiWeekly = 3,    // Bi-weekly (every two weeks) recurrence
        Monthly = 4,     // Monthly recurrence
        Yearly = 5,      // Yearly recurrence
        Custom = 6
    }
}