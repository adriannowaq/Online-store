using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Moq;
using OnlineStore.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTestOnlineStore
{
    public partial class ActiveTagHelperTests
    {
        [Theory]
        [MemberData(nameof(TestData))]
        public void Is_Adding_Active_Class_Properly(string fromController, 
                                                    string tagController,
                                                    string fromAction,
                                                    string tagAction, 
                                                    string tagAttributes,
                                                    string expectedTagAttributes,
                                                    string activeClass = null)
        {
            //arrange
            var activeTagHelper = new ActiveTagHelper()
            {
                Controller = tagController,
                Action = tagAction,
                ViewContext = new ViewContext()
                {
                    RouteData = new RouteData(new RouteValueDictionary()
                    {
                        { "controller", fromController },
                        { "action", fromAction }
                    })
                }
            };
            if (activeClass != null)
                activeTagHelper.ActiveClass = activeClass;

            var tagHelperContext = 
                new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            var tagHelperContent = new Mock<TagHelperContent>();
            var tagHelperOutput = new TagHelperOutput("a",
                new TagHelperAttributeList(new List<TagHelperAttribute>()
                {
                    new TagHelperAttribute("class", tagAttributes)
                }),
                (cache, encoder) => Task.FromResult(tagHelperContent.Object));

            //act
            activeTagHelper.Process(tagHelperContext, tagHelperOutput);

            //assert
            tagHelperOutput.Attributes.TryGetAttribute("class", out var attribute);
            Assert.Equal(expectedTagAttributes, attribute.Value);
        }
    }
}
