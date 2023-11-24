using Moq;
using NUnit.Framework;

namespace DevWorld.LaContessa.TestUtils.Utils;

public abstract class UnitTestBase
{
    protected CancellationTokenSource CancellationTokenSource;

    protected MockRepository MockRepository;
    protected CancellationToken CancellationToken => CancellationTokenSource.Token;

    [SetUp]
    public void SetUp()
    {
        MockRepository = new MockRepository(MockBehavior.Strict);
        CancellationTokenSource = new CancellationTokenSource();
    }

    [TearDown]
    public void Teardown()
    {
        MockRepository.VerifyAll();
        CancellationTokenSource.Dispose();
    }
}
