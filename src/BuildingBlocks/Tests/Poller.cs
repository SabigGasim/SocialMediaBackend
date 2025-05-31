namespace SocialMediaBackend.BuildingBlocks.Tests;
public class Poller(int timeoutMillis)
{
    private readonly int _timeoutMillis = timeoutMillis;

    private const int _pollDelayMillis = 1000;

    public async Task CheckAsync(IProbe probe)
    {
        var timeout = new Timeout(_timeoutMillis);
        do
        {
            if (timeout.HasTimedOut())
            {
                throw new AssertErrorException(DescribeFailureOf(probe));
            }

            await Task.Delay(_pollDelayMillis);
            await probe.SampleAsync();
        }
        while (!probe.IsSatisfied());
    }

    private static string DescribeFailureOf(IProbe probe)
    {
        return probe.DescribeFailureTo();
    }
}
