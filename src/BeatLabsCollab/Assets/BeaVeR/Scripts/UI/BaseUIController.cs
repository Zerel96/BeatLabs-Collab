using BeaVeR.Core;
using System;

namespace BeaVeR.UI
{
  public abstract class BaseUIController<TViewModel, TInput> : IController<TInput>
    where TViewModel : IViewModel, new()
    where TInput : IControllerInput
  {
    private TViewModel _viewModel;
    private bool _isActivated;

    private void Awake()
    {
      ViewModel = new TViewModel();

      DoAwake();
    }

    private void Start()
    {
      DoStart();
    }

    private void Update()
    {
      DoUpdate();
    }

    private void OnDestroy()
    {
      DoDestroy();
    }

    private void OnEnable()
    {
      DoOnEnable();
    }

    private void OnDisable()
    {
      DoOnDisable();
    }

    public void Activate(TInput input)
    {
      if (_isActivated)
      {
        return;
      }

      if (_viewModel == null)
      {
        throw new InvalidOperationException("ViewModel has not been created yet.");
      }

      DoActivate(input);

      _viewModel.IsVisible = true;

      _isActivated = true;
    }

    public void Deactivate()
    {
      if (!_isActivated)
      {
        return;
      }

      if (_viewModel == null)
      {
        throw new InvalidOperationException("ViewModel has not been created yet.");
      }

      DoDeactivate();

      _viewModel.IsVisible = false;

      _isActivated = false;
    }

    protected virtual void DoAwake()
    {
      // do nothing
    }

    protected virtual void DoStart()
    {
      // do nothing
    }

    protected virtual void DoUpdate()
    {
      // do nothing
    }

    protected virtual void DoDestroy()
    {
      // do nothing
    }

    protected virtual void DoOnEnable()
    {
      // do nothing
    }

    protected virtual void DoOnDisable()
    {
      // do nothing
    }

    protected virtual void DoActivate(TInput input)
    {
      // do nothing
    }

    protected virtual void DoDeactivate()
    {
      // do nothing
    }

    public TViewModel ViewModel
    {
      get => _viewModel;
      private set => _viewModel = value;
    }

    public bool IsActivated => _isActivated;
  }
}
