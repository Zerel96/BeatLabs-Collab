namespace BeaVeR.Core
{
  public static class Consts
  {
#if BL_QUEST
    public const string Platform = "Quest";
#elif BL_PICO
    public const string Platform = "Pico";
#endif
  }
}
