using Microsoft.AspNetCore.Mvc.Rendering;
using POSystem.Domain.Entities;

namespace POSystem.Web.Extensions;

public static class SupplierExtension
{
    public static List<SelectListItem> ToDropdownOption(this List<Supplier> suppliers)
    {
        return suppliers.Select(s => new SelectListItem
        {
            Value = s.Id.ToString(),
            Text = s.Name
        }).ToList();
    }
}
