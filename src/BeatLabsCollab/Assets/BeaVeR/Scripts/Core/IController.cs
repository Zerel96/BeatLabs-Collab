namespace BeaVeR.Core
{
  public interface IController<TInput>
    where TInput: IControllerInput
  {
    void Activate(TInput input);

    void Deactivate();

    bool IsActivated { get; }
  }
}
