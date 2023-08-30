namespace CRUDWithDifferentDB.PostgreSQL_Con.Models;

public partial class TblSectionInformation
{
    public long IntAutoId { get; set; }

    public string? StrBusinessUnit { get; set; }

    public string? StrSectionName { get; set; }

    public string? StrReadingOf { get; set; }

    public decimal NumReadingValue { get; set; }

    public DateOnly DteActionTime { get; set; }
}
