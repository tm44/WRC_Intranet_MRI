using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRI.Services.Budget
{
    public class BudgetRecord
    {
        [Required]
        [Column(TypeName = "char(6)")]
        public string Period  { get; set; }

        [Required]
        [Column(TypeName = "varchar(6)")]
        public string EntityID { get; set; }

        [Required]
        [Column(TypeName = "varchar(4)")]
        public string Department { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string AccountNumber { get; set; }

        [Required]
        [Column(TypeName = "varchar(1)")]
        public string Basis { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string BudgetType { get; set; }


        [Required]
        [Column(TypeName = "money")]
        public decimal Activity { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime LastDate { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string UserID { get; set; }
    }
}
