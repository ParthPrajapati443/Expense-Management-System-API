namespace Entity
{
    public class MonthlyExpenseEntity
    {
        public int MonthID { get; set; }
        public string UserName { get; set; }
        public int Month { get; set; }
        public long TotalCredit { get; set; }
        public long TotalDebit { get; set; }
        public long TotalBalence { get; set; }
    }
}
