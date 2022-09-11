using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum EventStatus
    {
        [Display(Name = "Не выбран")]
        None = 0,

        [Display(Name = "Ожидает подтверждения")]
        InProcess = 1,

        [Display(Name = "Активный")]
        Active = 2,

        [Display(Name = "Не активный")]
        InActive = 3
    }
}