
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameState : MonoBehaviour , IState
{

    [SerializeField] private GameObject _pauseScreen;
    private Button resumeButton;

    //public static Dictionary<GameObject, ISubject > pauseObserverDictionary = new Dictionary<GameObject,ISubject>();
    
    public void Enter()
    {
        _pauseScreen.SetActive(true);
        resumeButton = _pauseScreen.GetComponentInChildren<Button>();
        resumeButton.onClick.AddListener(HandlePauseButton);
        //PauseResumeGame(true);
       EventManager.TriggerPauseStateEvent(true);
    }


#if OBSERVER
    public static void Subscribe(ISubject subject,GameObject gmo)
    {
        pauseObserverDictionary.Add(gmo, subject);
    }

    public static void Unsubscribe(ISubject subject, GameObject gameObject)
    {
        if (pauseObserverDictionary.ContainsKey(gameObject))
        {
            pauseObserverDictionary.Remove(gameObject);
        }
    }

   private void PauseResumeGame(bool isGamePaused)
    {
        foreach(var item in pauseObserverDictionary)
        {
            item.Value.Notify(isGamePaused);
        }
    }   

#endif

    public void Exit()
    {
        _pauseScreen.SetActive(false);
        //PauseResumeGame(false);
        EventManager.TriggerPauseStateEvent(false);
    }

 

    private void HandlePauseButton()
    {
        GameManager.Instance.SetState(StateType.GameState);
    }
}
