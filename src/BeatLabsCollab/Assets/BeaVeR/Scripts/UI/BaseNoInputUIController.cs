using BeaVeR.Core;

namespace BeaVeR.UI
{
  public abstract class BaseNoInputUIController<TViewModel> : BaseUIController<TViewModel, EmptyControllerInput>
    where TViewModel : IViewModel, new()
  {
    public void Activate()
    {
      base.Activate(new EmptyControllerInput());
    }

    protected override void DoActivate(EmptyControllerInput input)
    {
      base.DoActivate(input);
      this.DoActivate();
    }

    protected virtual void DoActivate()
    {
      // do nothing
    }
  }
}
