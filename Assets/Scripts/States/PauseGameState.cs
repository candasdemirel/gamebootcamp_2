
using UnityEngine;
using UnityEngine.UI;

public class PauseGameState : MonoBehaviour , IState
{

    [SerializeField] private GameObject _pauseScreen;
    private Button resumeButton;
    
    public void Enter()
    {
        _pauseScreen.SetActive(true);
        resumeButton = _pauseScreen.GetComponentInChildren<Button>();
        resumeButton.onClick.AddListener(HandlePauseButton);
        EventManager.TriggerPauseStateEvent(true);
    }

    public void Exit()
    {
        _pauseScreen.SetActive(false);
        EventManager.TriggerPauseStateEvent(false);
    }


    private void HandlePauseButton()
    {
        GameManager.Instance.SetState(StateType.GameState);
    }
}
