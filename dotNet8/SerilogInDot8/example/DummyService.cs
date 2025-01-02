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
            logger.LogInformation("Invoking {@Event} with ID as {@Id}", "SomeEvent", Guid.NewGuid());
        }
        ///    config seq 
        ///    in cmd as admin 
        ///    - docker run --name seq -d -e ACCEPT_EULA=Y -p 80:80 -p 5341:5341 datalust/seq
        ///    then in same cmd 
        ///    - docker run --name View_Serilog_Messages -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest

    }
}
