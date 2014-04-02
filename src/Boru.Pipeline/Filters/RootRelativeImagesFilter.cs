using System;
using Fizzler.Systems.HtmlAgilityPack;

namespace Boru.Pipeline.Filters
{
  public class RootRelativeImagesFilter : AbstractFilter<RootRelativeImagesFilter.Parameters>
  {
    public class Parameters
    {
      public Uri BaseUri { get; set; }
    }

    public RootRelativeImagesFilter(Uri baseUri) : base()
    {
      Context.BaseUri = baseUri;
    }

    public override string Call(string text)
    {
      var doc = GetHtmlNode(text);
      foreach (var img in doc.QuerySelectorAll("img"))
      {
        if (img.Attributes["src"] == null) continue;
        var src = img.Attributes["src"].Value.Trim();
        if (!src.StartsWith("/")) continue;
        src = new Uri(Context.BaseUri, src).AbsoluteUri;
        img.Attributes["src"].Value = src;
      }

      return doc.OuterHtml;
    }
  }
}