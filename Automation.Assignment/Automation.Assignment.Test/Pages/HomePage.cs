using Automation.Assignment.Framework.Base;
using Automation.Assignment.Framework.Extensions;
using OpenQA.Selenium;

namespace Automation.Assignment.Test.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(ParallelConfig parallelConfig) : base(parallelConfig) { }


        #region Map of elements
        IList<IWebElement> MenuOptions => _parallelConfig.Driver.FindElements(By.CssSelector("#primary-menu a"));
        IList<IWebElement> ProductList => _parallelConfig.Driver.FindElements(By.CssSelector("ul.products li"));
        IList<IWebElement> AddToCartButtons => _parallelConfig.Driver.FindElements(By.CssSelector("a.add_to_cart_button"));
        IList<IWebElement> ViewCartButtons => _parallelConfig.Driver.FindElements(By.CssSelector("a.added_to_cart"));

        #endregion

        public void MenuNavigation(string menu) => MenuOptions.FirstOrDefault(x => x.Text.Equals(menu)).Click();

        public void AddProductsToCart(int numberOfProducts = 1)
        {
            if (AddToCartButtons.Count >= numberOfProducts)
            {
                IWebElement AddToCartBtn;
                int productCount = 0;
                while (productCount < numberOfProducts)
                {
                    ProductList[productCount].MouseHover(_parallelConfig.Driver);
                    AddToCartBtn = ProductList[productCount].FindElement(By.CssSelector("a.add_to_cart_button"));
                    if (AddToCartBtn != null)
                    {
                        _parallelConfig.Driver.ElementToBeClickable(AddToCartBtn).Click();
                        productCount++;
                    }
                }
            }
            else
            {
                throw new Exception("Number of products requested are not present to purchase.");
            }
        }

        public CartPage ClickViewCart()
        {
            _parallelConfig.Driver.WaitForAction();
            ViewCartButtons.FirstOrDefault().JClick(_parallelConfig.Driver);
            return new CartPage(_parallelConfig);
        }





    }
}
