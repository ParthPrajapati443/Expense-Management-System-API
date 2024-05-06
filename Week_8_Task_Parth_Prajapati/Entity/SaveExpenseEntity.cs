using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class SaveExpenseEntity
    {
        public long ExpenseID { get; set; }
        [Required(ErrorMessage = "Enert your mail.")]
        public string ExpenseEmail { get; set; }
        [Required(ErrorMessage = "Enert Credit or Debit.")]
        public string ExpenseType { get; set; }
        [Required(ErrorMessage = "Enert your expense amount.")]
        //[Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal ExpenseAmount { get; set; }
        public string ExpenseReason { get; set; }
        public string ExpenseDate { get; set; }
        public long TotalBalence { get; set; }
    }
}
