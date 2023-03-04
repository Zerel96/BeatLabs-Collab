namespace BeaVeR.UI
{
  public class BaseViewModel : IViewModel
  {
    private bool _isVisible;

    public bool IsVisible
    {
      get => _isVisible;
      set => _isVisible = value;
    }
  }
}
