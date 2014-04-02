namespace Boru.Pipeline.Filters
{
  public class NewlineToBrFilter : AbstractFilter<NoParameters>
  {
    public override string Call(string text)
    {
      return text.Replace("\n", "<br>");
    }
  }
}