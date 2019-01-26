using System;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Helpers.Extensions
{
    public static class UserMenuExtensions
    {
        public static bool IsMenuActive(this IHtmlHelper htmlHelper, string menuItemUrl)
        {
            var viewContext = htmlHelper.ViewContext;
            var currentPageUrl =
                viewContext.ViewData["ActiveMenu"] as string ?? viewContext.HttpContext.Request.Path;

            if (menuItemUrl.IsNullOrEmpty())
            {
                return !(currentPageUrl.Length > 1);
            }

            return currentPageUrl.StartsWith(menuItemUrl, StringComparison.OrdinalIgnoreCase);
        }
    }
}