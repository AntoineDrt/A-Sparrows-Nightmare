
using UnityEngine.Events;

public class TurnPhase<T> : TurnPhase
{
  public new UnityEvent<T> Start;

  public TurnPhase()
  {
    Start = new UnityEvent<T>();
  }

  public void AddListener(UnityAction<T> listener)
  {
    Start.AddListener(listener);
    ListenersCount++;
  }

  public void RemoveListener(UnityAction<T> listener)
  {
    Start.RemoveListener(listener);
    ListenersCount--;
  }
}

public class TurnPhase
{
  public UnityEvent Start;
  public UnityEvent End;

  public int ListenersCount = 0;
  public int ListenersFinishedCount = 0;

  public TurnPhase()
  {
    Start = new UnityEvent();
    End = new UnityEvent();
  }

  public void AddListener(UnityAction listener)
  {
    Start.AddListener(listener);
    ListenersCount++;
  }

  public void RemoveListener(UnityAction listener)
  {
    Start.RemoveListener(listener);
    ListenersCount--;
  }

  public void Done()
  {
    ListenersFinishedCount++;

    if (ListenersFinishedCount == ListenersCount)
    {
      End.Invoke();
      ListenersFinishedCount = 0;
    }
  }
}