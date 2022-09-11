using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum UserStatus
    {
        [Display(Name = "В обработке")]
        InProcess = 0,

        [Display(Name = "Подтвержден")]
        Active = 1,

        [Display(Name = "Заблокирован")]
        Blocked = 2,
    }
}