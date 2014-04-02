using HtmlAgilityPack;

namespace Boru.Pipeline.Filters
{
  public class PlainTextInputFilter : AbstractFilter<NoParameters>
  {
    public override string Call(string text)
    {
      var doc = new HtmlDocument();
      var div = doc.CreateElement("div");
      div.InnerHtml = HtmlDocument.HtmlEncode(text);
      doc.DocumentNode.AppendChild(div);
      return doc.DocumentNode.OuterHtml;
    }
  }
}