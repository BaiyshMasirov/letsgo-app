using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum TicketStatus
    {
        [Display(Name = "Ожидает оплату")]
        InProccess = 0,

        [Display(Name = "Оплачен")]
        Payed = 1,

        [Display(Name = "Возвращен")]
        Returned = 2,

        [Display(Name = "Использован")]
        Used = 3
    }
}