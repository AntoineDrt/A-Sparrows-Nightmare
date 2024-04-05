
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TurnManager : MonoBehaviour
{
  public static TurnManager Instance;

  public TurnPhase<CallbackContext> MovePhase;
  public TurnPhase ActionPhase;

  private bool MoveInputsEnabled = true;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    MapManager.Instance.MapGenerated.AddListener(() => MoveInputsEnabled = true);

    MovePhase = new TurnPhase<CallbackContext>();
    ActionPhase = new TurnPhase();

    InputManager.playerInput.performed += StartTurn;

    MovePhase.End.AddListener(OnMovePhaseDone);
    ActionPhase.End.AddListener(OnActionPhaseDone);
  }

  private void OnMovePhaseDone()
  {
    if (ActionPhase.ListenersCount > 0)
    {
      ActionPhase.Start.Invoke();
    }
    else
    {
      ActionPhase.End.Invoke();
    }
  }

  private void OnActionPhaseDone()
  {
    MoveInputsEnabled = true;
  }

  private void StartTurn(CallbackContext moveContext)
  {
    if (!MoveInputsEnabled) return;

    MoveInputsEnabled = false;

    MovePhase.Start.Invoke(moveContext);
  }
}

