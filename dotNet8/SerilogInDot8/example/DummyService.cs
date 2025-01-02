namespace SerilogInDot8.example
{
    public class DummyService : IDummyService
    {
        private readonly ILogger<DummyService> logger;
        public DummyService(ILogger<DummyService> logger)
        {
            this.logger = logger;
        }

        public void DoSomething()
        {
            logger.LogInformation("something is done");
            logger.LogCritical("oops");
            logger.LogDebug("nothing much");
        }
    }
}
