using Automation.Assignment.Framework.Base;
using Automation.Assignment.Framework.Extensions;
using OpenQA.Selenium;

namespace Automation.Assignment.Test.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(ParallelConfig parallelConfig) : base(parallelConfig) { }

        #region Map of elements
        IList<IWebElement> ProductTableRows => _parallelConfig.Driver.FindElements(By.CssSelector("table.shop_table.cart tr"));
        IList<IWebElement> ProductTablePriceCol => _parallelConfig.Driver.FindElements(By.CssSelector("table.shop_table.cart td.product-price"));
        IList<IWebElement> ProductTableRemoveButtons => _parallelConfig.Driver.FindElements(By.CssSelector("td.product-remove a"));

        #endregion


        public int GetCartProductCount()
        {
            _parallelConfig.Driver.WaitForPageLoaded();
            return ProductTableRows.Count() - 2;
        }

        public int GetLowestPriceItemIndex()
        {
            List<decimal> productPrices = new List<decimal>();
            foreach (var product in ProductTablePriceCol)
            {
                productPrices.Add(Convert.ToDecimal(product.Text.Remove(0, 1)));
            }

            var sorted = productPrices
                         .Select((x, i) => new KeyValuePair<decimal, int>(x, i))
                         .OrderBy(x => x.Key)
                         .ToList();

            List<int> index = sorted.Select(x => x.Value).ToList();
            return index[0];
        }

        public void RemoveItemFromCart(int index)
        {
            _parallelConfig.Driver.ElementToBeClickable(ProductTableRemoveButtons[index]).Click();
            _parallelConfig.Driver.WaitForPageLoaded();
            _parallelConfig.Driver.WaitForAction();
        }
    }
}
