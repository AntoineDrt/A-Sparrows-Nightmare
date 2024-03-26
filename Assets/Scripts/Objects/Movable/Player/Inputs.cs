
using System;
using UnityEngine.InputSystem;

public static class InputsInitializer {
  public static InputAction InitMoveAction(Action<InputAction.CallbackContext> OnMovePerformed) {
    var playerInput = new InputAction("Move");
    playerInput.AddCompositeBinding("Dpad")
        .With("Up", "<Keyboard>/upArrow")
        .With("Down", "<Keyboard>/downArrow")
        .With("Left", "<Keyboard>/leftArrow")
        .With("Right", "<Keyboard>/rightArrow");

    playerInput.performed += OnMovePerformed;
    playerInput.Enable();

    return playerInput;
  }
}