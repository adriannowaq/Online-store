using System.Collections.Generic;

namespace XUnitTestOnlineStore
{
    public partial class ActiveTagHelperTests
    {
        public static IList<object[]> TestData =>
            new List<object[]>
            {
                new object[]
                {
                    //fromController
                    "Home",
                    //tagController
                    "Home",
                    //fromAction
                    "Index",
                    //tagAction
                    "Index",
                    //tagAttributes
                    "mx-auto",
                    //expectedTagAttributes
                    "mx-auto active"
                },
                new object[]
                {
                    //fromController
                    "Account",
                    //tagController
                    "Home",
                    //fromAction
                    "Index",
                    //tagAction
                    "Index",
                    //tagAttributes
                    "mx-auto",
                    //expectedTagAttributes
                    "mx-auto"
                },
                new object[]
                {
                    //fromController
                    "Home",
                    //tagController
                    "Home",
                    //fromAction
                    "Contact",
                    //tagAction
                    "Index",
                    //tagAttributes
                    "mx-auto",
                    //expectedTagAttributes
                    "mx-auto"
                },
                new object[]
                {
                    //fromController
                    "Home",
                    //tagController
                    "Home",
                    //fromAction
                    "Contact",
                    //tagAction
                    "Index",
                    //tagAttributes
                    "mx-auto",
                    //expectedTagAttributes
                    "mx-auto",
                    //activeClass
                    "light"
                },
                new object[]
                {
                    //fromController
                    "Account",
                    //tagController
                    "Account",
                    //fromAction
                    "Login",
                    //tagAction
                    "Login",
                    //tagAttributes
                    "mx-auto p-0 mt-1",
                    //expectedTagAttributes
                    "mx-auto p-0 mt-1 light",
                    //activeClass
                    "light"
                }
            };
    }
}
