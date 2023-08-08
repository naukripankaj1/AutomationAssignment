namespace Automation.Assignment.Framework.Base
{
    public class Base
    {
        public readonly ParallelConfig _parallelConfig;

        public Base(ParallelConfig parellelConfig)
        {
            _parallelConfig = parellelConfig;
        }

        public TPage As<TPage>() where TPage : BasePage
        {
            return (TPage)this;
        }
    }
}
