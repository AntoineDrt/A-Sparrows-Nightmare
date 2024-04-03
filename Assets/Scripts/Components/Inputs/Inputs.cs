
using System;
using UnityEngine.InputSystem;

public static class InputsInitializer {
  public static InputAction InitMoveAction(Action<InputAction.CallbackContext> OnMovePerformed) {
    var playerInput = new InputAction("Move");
    playerInput.AddCompositeBinding("Dpad")
        .With("Up", "<Keyboard>/upArrow")
        .With("Down", "<Keyboard>/downArrow")
        .With("Left", "<Keyboard>/leftArrow")
        .With("Right", "<Keyboard>/rightArrow")
        .With("Up", "<Keyboard>/w")
        .With("Down", "<Keyboard>/s")
        .With("Left", "<Keyboard>/a")
        .With("Right", "<Keyboard>/d");

    playerInput.performed += OnMovePerformed;
    playerInput.Enable();

    return playerInput;
  }
}