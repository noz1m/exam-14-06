namespace Domain.Filter;

public class SaleFilter : ValidFilter
{
    public DateTime? MaxDate { get; set; }
    public DateTime? MinDate { get; set; }
    public int? ProductId { get; set; }
}
