using Boru.Pipeline.Filters;
using Xunit;

namespace Boru.Pipeline.Tests.FilterTests
{
  public class PlainTextInputFilterTests
  {
    [Fact]
    public void Call_Should_WrapInputInADivElement()
    {
      var input = "howdy partner";
      var result = new PlainTextInputFilter().Call(input);
      Assert.Equal("<div>howdy partner</div>", result);
    }

    [Fact]
    public void Call_Should_EscapeInput()
    {
      var input = "See: <http://example.org>";
      var result = new PlainTextInputFilter().Call(input);
      Assert.Equal("<div>See: &lt;http://example.org&gt;</div>", result);
    }
  }
}