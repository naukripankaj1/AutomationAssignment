using Automation.Assignment.Framework.Base;
using Automation.Assignment.Test.Pages;
using NUnit.Framework;

namespace Automation.Assignment.Test.StepDefinitions
{
    [Binding]
    internal class UpdatedProductsToCartSteps : BaseStep
    {
        private readonly ScenarioContext scenarioContext;
        public UpdatedProductsToCartSteps(ParallelConfig parallelConfig, ScenarioContext scenarioContext) : base(parallelConfig)
        {
            this.scenarioContext = scenarioContext ?? throw new ArgumentNullException("scenarioContext");
        }

        [Given(@"I add (.*) random items to my cart.")]
        public void GivenIAddRandomItemsToMyCart(int itemCount)
        {
            _parallelConfig.CurrentPage.As<HomePage>().AddProductsToCart(itemCount);
        }

        [Then(@"I am able to verify (.*) items in my cart.")]
        [Then(@"I find total (.*) items listed in my cart.")]
        public void ThenIFindTotalItemsListedInMyCart(int itemCount)
        {
            Assert.AreEqual(itemCount, _parallelConfig.CurrentPage.As<CartPage>().GetCartProductCount(), "Number of products in cart doesn't displayed as expected.");
        }
    }
}
