using System.ComponentModel.DataAnnotations;

namespace Entity
{
    public class UserEntity
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        [StringLength(15, MinimumLength = 2)]
        [RegularExpression("^[0-9]*$")]
        public string UserPhone { get; set; }
        public string UserAddress { get; set; }
        public string UserPassword { get; set; }
        public bool IsActive { get; set; }
        public string Flag { get; set; }
    }
}
