namespace CRUDWithDifferentDB.PostgreSQL_Con
{
    public class CreateSectionInformationsDTO
    {
        public long AutoId { get; set; }

        public string BusinessUnit { get; set; }=string.Empty;

        public string SectionName { get; set; } = string.Empty;

        public string ReadingOf { get; set; } = string.Empty;

        public decimal ReadingValue { get; set; }

        public DateOnly DteActionTime { get; set; }
    }
}
