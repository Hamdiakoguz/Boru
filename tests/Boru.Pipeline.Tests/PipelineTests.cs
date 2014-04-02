using System;
using System.Linq;
using Boru.Pipeline.Filters;
using NSubstitute;
using Xunit;

namespace Boru.Pipeline.Tests
{
  public class PipelineTests
  {
    [Fact]
    public void SetFilters()
    {
      var pipeline = new Pipeline(
        new PlainTextInputFilter(),
        new NewlineToBrFilter(),
        new RootRelativeImagesFilter(new Uri("http://somehost.com")));

      Assert.Equal(3, pipeline.Filters.Count());
    }

    [Fact]
    public void Call_Should_Call_Each_Filter_In_Turn()
    {
      var filters = new[]
      {
        Substitute.For<IFilter>(),
        Substitute.For<IFilter>(),
        Substitute.For<IFilter>()
      };
      filters[0].Call("1").Returns("2");
      filters[1].Call("2").Returns("3");
      filters[2].Call("3").Returns("4");

      var pipeline = new Pipeline(filters);
      var result = pipeline.Call("1");

      foreach (var filter in filters)
      {
        filter.Received().Call(Arg.Any<string>());
      }
      Assert.Equal("4", result);
    }
  }
}