using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum LocationStatus
    {
        [Display(Name = "Не выбран")]
        None = 0,

        [Display(Name = "Активный")]
        Active = 1,

        [Display(Name = "Не активный")]
        InActive = 2
    }
}