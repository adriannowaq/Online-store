using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using XUnitTestOnlineStore.Data;
using OnlineStore.Infrastructure.Helpers;

namespace XUnitTestOnlineStore
{
    public class ActiveTagHelperTests
    {
        [Theory]
        [ClassData(typeof(ActiveTagHelperTestsData))]
        public void Is_Adding_Active_Class_Properly(ActiveTagHelperTestModel testModel)
        {
            //arrange
            var activeTagHelper = new ActiveTagHelper()
            {
                Controllers = testModel.TagControllers,
                Actions = testModel.TagActions,
                ViewContext = new ViewContext()
                {
                    RouteData = new RouteData(new RouteValueDictionary()
                    {
                        { "controller", testModel.FromController },
                        { "action", testModel.FromAction }
                    })
                }
            };
            if (testModel.ActiveClasses != null)
                activeTagHelper.ActiveClasses = testModel.ActiveClasses;

            var tagHelperContext = 
                new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");

            var tagHelperContent = new Mock<TagHelperContent>();
            var tagHelperOutput = new TagHelperOutput("",
                new TagHelperAttributeList(new List<TagHelperAttribute>()
                {
                    new TagHelperAttribute("class", testModel.TagAttributes)
                }),
                (cache, encoder) => Task.FromResult(tagHelperContent.Object));

            //act
            activeTagHelper.Process(tagHelperContext, tagHelperOutput);

            //assert
            tagHelperOutput.Attributes.TryGetAttribute("class", out var attribute);
            Assert.Equal(testModel.ExpectedClassAttributes, attribute.Value);
        }
    }
}
