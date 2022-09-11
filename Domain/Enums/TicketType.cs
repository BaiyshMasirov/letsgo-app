using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    public enum TicketType
    {
        [Display(Name = "Простой")]
        Simple = 0,

        [Display(Name = "Комфорт")]
        Comfort = 1,

        [Display(Name = "ВИП")]
        Vip = 2
    }
}