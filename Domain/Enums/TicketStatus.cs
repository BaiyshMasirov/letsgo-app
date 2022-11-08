using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum TicketStatus
    {
        [Display(Name = "Свободен")]
        Free = 0,

        [Display(Name = "Ожидает оплату")]
        InProccess = 1,

        [Display(Name = "Оплачен")]
        Payed = 2,

        [Display(Name = "Возвращен")]
        Returned = 3,

        [Display(Name = "Использован")]
        Used = 4
    }
}