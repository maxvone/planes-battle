using UnityEngine;

namespace CodeBase.Services.Input
{
  public class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string Button = "Fire";

    public Vector2 Axis => UnityAxis();

    public bool IsAttackButtonUp()
    {
      return UnityEngine.Input.GetButtonUp(Button);
    }

    private static Vector2 UnityAxis()
    {
      return new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
  }
}