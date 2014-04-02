using HtmlAgilityPack;

namespace Boru.Pipeline.Filters
{
  public abstract class AbstractFilter<TParameters> : IFilter where TParameters : new()
  {
    public TParameters Context { get; set; }

    protected AbstractFilter()
    {
      Context = new TParameters();
    }

    public abstract string Call(string text);

    public HtmlDocument ParseHtml(string text)
    {
      var html = new HtmlDocument();
      html.LoadHtml(text);
      return html;
    }

    public HtmlNode GetHtmlNode(string text)
    {
      return ParseHtml(text).DocumentNode;
    }
  }
}