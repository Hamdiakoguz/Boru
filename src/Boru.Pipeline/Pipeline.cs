using System.Collections.Generic;
using Boru.Pipeline.Filters;

namespace Boru.Pipeline
{
  public class Pipeline : IPipeline
  {
    public IEnumerable<IFilter> Filters { get; private set; }

    public Pipeline(params IFilter[] filters)
    {
      Filters = filters;
    }

    public string Call(string text)
    {
      var filtered = text;
      foreach (var filter in Filters)
      {
        filtered = filter.Call(filtered);
      }
      return filtered;
    }
  }
}