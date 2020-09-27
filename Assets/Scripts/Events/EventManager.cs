
public static class EventManager
{
    public delegate void DamgeEvent(int damage);

    public static event DamgeEvent DamageEventResult;

    public static void TriggerDamageEventResult(int damage)
    {
        DamageEventResult?.Invoke(damage);
    }

    public delegate void ScoreEvent(int score);

    public static event ScoreEvent ScoreEventResult;

    public static void TriggerScoreEventResult(int score)
    {
        ScoreEventResult?.Invoke(score);
    }

    public delegate void HPChangeEvent(int hp);

    public static event HPChangeEvent HPChangeEventResult;

    public static void TriggerHPChangeEventResult(int hp)
    {
        HPChangeEventResult?.Invoke(hp);
    }


    public delegate void PauseEvent(bool isPaused);

    public static event PauseEvent PauseStateEvent;

    public static void TriggerPauseStateEvent(bool isPaused)
    {
        PauseStateEvent?.Invoke(isPaused);
    }
}
