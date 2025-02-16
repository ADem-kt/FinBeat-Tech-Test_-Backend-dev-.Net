namespace FinBeat_Tech_Test__Backend_dev_.Net.ef
{
    public class DBLoggerClass
    {
        public int row_id { get; }
        public DateTime dateTime { get; set; } = DateTime.Now;
        public string? savedMesseges { get; set; }
    };
}
