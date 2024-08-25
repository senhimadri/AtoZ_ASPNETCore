using UnitTestingExe.Services;
using Xunit;

namespace UnitTestingExe.TastCase;

public class CalculatorTest
{
    [Fact]
    public void Add_PositiveNumbers_ReturnsExpectedResult()
    {
        var claculator = new Calculator();
        int a = 1, b = 2;

        int expectedResult  = 3;
        int actualResult = claculator.Add(a,b);

        Assert.Equal(expectedResult, actualResult);
    }
}
