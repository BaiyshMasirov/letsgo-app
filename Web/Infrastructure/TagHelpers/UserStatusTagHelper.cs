using Application.MediatR.Admins.Accounts.Queries.GetAdmins;
using Domain.Enums;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.Infrastructure.TagHelpers
{
    public class UserStatusTagHelper : TagHelper
    {
        public AdminDto User { get; set; }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            (string, string) GetStatusTextAndBackgroundColor()
            {
                if (User.Status == UserStatus.InProcess)
                {
                    return ("Не подтвержден", "badge-light");
                }
                else if (User.LockoutEnabled)
                {
                    return ("Заблокирован", "badge-danger");
                }
                else
                {
                    return ("Активный", "badge-primary");
                }
            }

            (string text, string bgcolor) = GetStatusTextAndBackgroundColor();

            output.TagName = "span";
            output.Attributes.SetAttribute("class", $"badge badge-pill p-1 text-white {bgcolor}");
            output.Content.Append(text);

            return base.ProcessAsync(context, output);
        }
    }
}