using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace OnlineStore.Infrastructure
{
    [HtmlTargetElement("a", Attributes = "route-active-controller, route-active-action")]
    public class ActiveTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeName("route-active-controller")]
        public string Controller { get; set; }

        [HtmlAttributeName("route-active-action")]
        public string Action { get; set; }

        [HtmlAttributeName("route-active-class")]
        public string ActiveClass { get; set; } = "active";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (string.IsNullOrWhiteSpace(Controller) || string.IsNullOrWhiteSpace(Action))
                return;

            var currentController = ViewContext.RouteData.Values["controller"].ToString();
            var currentAction = ViewContext.RouteData.Values["action"].ToString();

            if (currentController.Equals(Controller) && currentAction.Equals(Action))
            {
                if (output.Attributes.ContainsName("class"))
                    output.Attributes.SetAttribute("class", $"{output.Attributes["class"].Value} {ActiveClass}");
                else
                    output.Attributes.SetAttribute("class", ActiveClass);
            }
        }
    }
}
