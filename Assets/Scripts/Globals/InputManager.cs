
using UnityEngine.InputSystem;

public static class InputManager
{
  public static InputAction playerInput = InitMoveAction();

  public static InputAction InitMoveAction()
  {
    var inputs = new InputAction("Move");
    inputs.AddCompositeBinding("Dpad")
        .With("Up", "<Keyboard>/upArrow")
        .With("Down", "<Keyboard>/downArrow")
        .With("Left", "<Keyboard>/leftArrow")
        .With("Right", "<Keyboard>/rightArrow")
        .With("Up", "<Keyboard>/w")
        .With("Down", "<Keyboard>/s")
        .With("Left", "<Keyboard>/a")
        .With("Right", "<Keyboard>/d");

    inputs.Enable();

    return inputs;
  }
}