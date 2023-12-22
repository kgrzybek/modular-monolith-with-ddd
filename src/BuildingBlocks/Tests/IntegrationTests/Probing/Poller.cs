namespace CompanyName.MyMeetings.BuildingBlocks.IntegrationTests.Probing
{
    public class Poller
    {
        private readonly int _timeoutMillis;

        private readonly int _pollDelayMillis;

        public Poller(int timeoutMillis)
        {
            _timeoutMillis = timeoutMillis;
            _pollDelayMillis = 1000;
        }

        public async Task CheckAsync(IProbe probe)
        {
            var timeout = new Timeout(_timeoutMillis);
            while (!probe.IsSatisfied())
            {
                if (timeout.HasTimedOut())
                {
                    throw new AssertErrorException(DescribeFailureOf(probe));
                }

                await Task.Delay(_pollDelayMillis);
                await probe.SampleAsync();
            }
        }

        public async Task<T> GetAsync<T>(IProbe<T> probe)
            where T : class
        {
            var timeout = new Timeout(_timeoutMillis);
            T sample = null;
            while (!probe.IsSatisfied(sample))
            {
                if (timeout.HasTimedOut())
                {
                    throw new AssertErrorException(DescribeFailureOf(probe));
                }

                await Task.Delay(_pollDelayMillis);
                sample = await probe.GetSampleAsync();
            }

            return sample;
        }

        private static string DescribeFailureOf(IProbe probe)
        {
            return probe.DescribeFailureTo();
        }

        private static string DescribeFailureOf<T>(IProbe<T> probe)
        {
            return probe.DescribeFailureTo();
        }
    }
}