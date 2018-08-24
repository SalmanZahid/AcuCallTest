using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Linq;

namespace AcuCall.Web.TagHelpers
{
    [HtmlTargetElement(Attributes = "asp-active-route")]
    public class ActiveRouteTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var currentController = ViewContext.RouteData.Values["Controller"].ToString();

            if (!String.IsNullOrWhiteSpace(Controller) && Controller.Equals(currentController, StringComparison.CurrentCultureIgnoreCase))
            {
                MakeActive(output);
            }
            else
            {
                MakeInActive(output);
            }

            var attribute = output.Attributes.First(x => x.Name == "asp-active-route");
            output.Attributes.Remove(attribute);
        }

        private void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttr);
            }
            else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf("active") < 0)
            {
                output.Attributes.SetAttribute("class", classAttr.Value == null
                    ? "active"
                    : classAttr.Value.ToString() + " active");
            }
        }

        private void MakeInActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");

            if (classAttr == null) return;

            if (classAttr.Value != null && classAttr.Value.ToString().IndexOf("active") > 0)
            {
                output.Attributes.SetAttribute("class", classAttr.Value.ToString().Replace("active", ""));
            }
        }
    }
}
