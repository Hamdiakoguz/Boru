namespace Boru.Pipeline.Filters
{
  public interface IFilter
  {
    string Call(string text);
  }

  public class NoParameters
  {
  }
}