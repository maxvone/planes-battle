using UnityEngine;

namespace CodeBase.Services.Input
{
  public class InputService : IInputService
  {
    protected const string Horizontal = "Horizontal";
    protected const string Vertical = "Vertical";
    private const string Button = "Fire1";

    public Vector2 Axis => UnityAxis();

    public bool IsAttackButtonDown() => 
      UnityEngine.Input.GetButtonDown(Button);

    private static Vector2 UnityAxis() => 
      new Vector2(UnityEngine.Input.GetAxisRaw(Horizontal), UnityEngine.Input.GetAxisRaw(Vertical));
  }
}