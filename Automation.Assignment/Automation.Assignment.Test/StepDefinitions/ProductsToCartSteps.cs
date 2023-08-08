using Automation.Assignment.Framework.Base;
using Automation.Assignment.Test.Pages;
using NUnit.Framework;

namespace Automation.Assignment.Test.StepDefinitions
{
    [Binding]
    internal class ProductsToCartSteps : BaseStep
    {
        private readonly ScenarioContext scenarioContext;
        public ProductsToCartSteps(ParallelConfig parallelConfig, ScenarioContext scenarioContext) : base(parallelConfig)
        {
            this.scenarioContext = scenarioContext ?? throw new ArgumentNullException("scenarioContext");
        }

        [Given(@"I add four random items to my cart")]
        public void GivenIAddFourRandomItemsToMyCart()
        {
            _parallelConfig.CurrentPage.As<HomePage>().AddProductsToCart(4);
        }

        [When(@"I view my cart")]
        public void WhenIViewMyCart()
        {
            _parallelConfig.CurrentPage = _parallelConfig.CurrentPage.As<HomePage>().ClickViewCart();
        }

        [Then(@"I find total four items listed in my cart")]
        public void ThenIFindTotalFourItemsListedInMyCart()
        {
            Assert.AreEqual(4, _parallelConfig.CurrentPage.As<CartPage>().GetCartProductCount(), "Number of products in cart doesn't displayed as expected.");
        }

        [When(@"I search for lowest price item")]
        public void WhenISearchForLowestPriceItem()
        {
            //no search functionality on Cart page so created method to get lowest price item index
            scenarioContext.Set<int>(_parallelConfig.CurrentPage.As<CartPage>().GetLowestPriceItemIndex(), "LowestPriceItemIndex");
        }

        [When(@"I am able to remove the lowest price item from my cart")]
        public void WhenIAmAbleToRemoveTheLowestPriceItemFromMyCart()
        {
            _parallelConfig.CurrentPage.As<CartPage>().RemoveItemFromCart(scenarioContext.Get<int>("LowestPriceItemIndex"));
        }

        [Then(@"I am able to verify three items in my cart")]
        public void ThenIAmAbleToVerifyThreeItemsInMyCart()
        {
            Assert.AreEqual(3, _parallelConfig.CurrentPage.As<CartPage>().GetCartProductCount(), "Number of products in cart doesn't displayed as expected.");
        }
    }
}
