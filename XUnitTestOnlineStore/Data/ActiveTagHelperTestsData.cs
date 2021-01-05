using System.Collections;
using System.Collections.Generic;

namespace XUnitTestOnlineStore.Data
{
    public class ActiveTagHelperTestsData : IEnumerable<object[]>
    {
        private readonly IList<object[]> testData = new List<object[]>
        {
            new object[]
            {
                new ActiveTagHelperTestModel
                {
                    FromController = "Home",
                    FromAction = "Index",
                    TagControllers = "Home",
                    TagActions = "Index",
                    TagAttributes = "mx-auto",
                    ExpectedClassAttributes = "mx-auto active"
                }
            },
            new object[]
            {
                new ActiveTagHelperTestModel
                {
                    FromController = "Account",
                    FromAction = "Index",
                    TagControllers = "Home",
                    TagActions = "Index",
                    TagAttributes = "mx-auto",
                    ExpectedClassAttributes = "mx-auto"
                }
            },
            new object[]
            {
                new ActiveTagHelperTestModel
                {
                    FromController = "Home",
                    FromAction = "Contact",
                    TagControllers = "Home",
                    TagActions = "Index",
                    TagAttributes = "mx-auto",
                    ExpectedClassAttributes = "mx-auto"
                }
            },
            new object[]
            {
                new ActiveTagHelperTestModel
                {
                    FromController = "Home",
                    FromAction = "Contact",
                    TagControllers = "Home",
                    TagActions = "Index",
                    TagAttributes = "mx-auto",
                    ExpectedClassAttributes = "mx-auto",
                    ActiveClasses = "light"
                }
            },
            new object[]
            {
                new ActiveTagHelperTestModel
                {
                    FromController = "Account",
                    FromAction = "Login",
                    TagControllers = "Account",
                    TagActions = "Login",
                    TagAttributes = "mx-auto p-0 mt-1",
                    ExpectedClassAttributes = "mx-auto p-0 mt-1 light",
                    ActiveClasses = "light"
                }
            },
            new object[]
            {
                new ActiveTagHelperTestModel
                {
                    FromController = "Account",
                    FromAction = "Index",
                    TagControllers = "Account",
                    TagAttributes = "mx-auto p-0 mt-1",
                    ExpectedClassAttributes = "mx-auto p-0 mt-1 light",
                    ActiveClasses = "light"
                }
            },
            new object[]
            {
                new ActiveTagHelperTestModel
                {
                    FromController = "Home",
                    FromAction = "Index",
                    TagControllers = "Account",
                    TagAttributes = "mx-auto",
                    ExpectedClassAttributes = "mx-auto",
                    ActiveClasses = "light"
                }
            },
            new object[]
            {
                new ActiveTagHelperTestModel
                {
                    FromController = "Account",
                    FromAction = "Details",
                    TagControllers = "Home Account",
                    TagAttributes = "mx-auto",
                    ExpectedClassAttributes = "mx-auto active"
                }
            },
            new object[]
            {
                new ActiveTagHelperTestModel
                {
                    FromController = "Home",
                    FromAction = "Contact",
                    TagControllers = "Home Account",
                    TagAttributes = "mx-auto",
                    ExpectedClassAttributes = "mx-auto active"
                }
            }
        };

        public IEnumerator<object[]> GetEnumerator() => testData.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
