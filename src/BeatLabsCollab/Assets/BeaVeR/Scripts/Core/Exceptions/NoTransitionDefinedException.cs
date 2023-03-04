using System;

namespace BeaVeR.Core.Exceptions
{
  public class NoTransitionDefinedException : Exception
  {
    public NoTransitionDefinedException(string currentStateName, string newStateName)
      : base($"No transition defined from '{currentStateName}' to '{newStateName}'.")
    {
    }
  }
}
