using System.Threading;

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

        public void Check(IProbe probe)
        {
            var timeout = new Timeout(_timeoutMillis);
            while (!probe.IsSatisfied())
            {
                if (timeout.HasTimedOut())
                {
                    throw new AssertErrorException(DescribeFailureOf(probe));
                }

                Thread.Sleep(_pollDelayMillis);
                probe.SampleAsync();
            }
        }

        private static string DescribeFailureOf(IProbe probe)
        {
            return probe.DescribeFailureTo();
        }
    }
}