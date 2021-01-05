using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace OnlineStore.Infrastructure.Helpers
{
    [HtmlTargetElement("a", Attributes = "route-active-controllers")]
    public class ActiveTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("route-active-controllers")]
        public string Controllers { get; set; }

        [HtmlAttributeName("route-active-actions")]
        public string Actions { get; set; }

        [HtmlAttributeName("route-active-classes")]
        public string ActiveClasses { get; set; } = "active";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(Controllers))
                return;

            var currentController = ViewContext.RouteData.Values["controller"].ToString();
            var currentAction = ViewContext.RouteData.Values["action"].ToString();

            if (string.IsNullOrWhiteSpace(Actions))
                currentAction = null;

            if (Controllers.Split(" ").Contains(currentController) == true && 
                 (Actions == null || Actions.Split(" ").Contains(currentAction) == true))
            {
                if (output.Attributes.ContainsName("class"))
                    output.Attributes.SetAttribute("class", $"{output.Attributes["class"].Value} {ActiveClasses}");
                else
                    output.Attributes.SetAttribute("class", ActiveClasses);
            }
        }
    }
}
