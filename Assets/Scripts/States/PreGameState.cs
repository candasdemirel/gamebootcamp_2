using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PreGameState : MonoBehaviour, IState
{
    [SerializeField] private GameObject _waitScreen;
    [SerializeField] private TextMeshProUGUI _waitText;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    private bool _isWaitingToStart;
    private Coroutine _coroutine;
    private InputController _inputController;


    public void Enter()
    {
        Debug.Log("Entered pregame game state");
        _inputController = new InputController(HandleInputResult);

        _isWaitingToStart = true;
        _coroutine = StartCoroutine(WaitForStart());
        _waitScreen.SetActive(true);
        _bestScoreText.text = GameManager.Instance.GetMaxScore().ToString();
    }

    private void HandleInputResult()
    {
        GameManager.Instance.SetState(StateType.GameState);
    }

    public void Exit()
    {
        _isWaitingToStart = false;
        _inputController = null;
        StopCoroutine(_coroutine);
        _waitScreen.SetActive(false);
    }

    private void Update()
    {
        if (GameManager.Instance.GetCurrentState() != this) return;

        _inputController?.GetInput();
    }


    private IEnumerator WaitForStart()
    {
        float t = 0;

        while (_isWaitingToStart)
        {
            var val = Mathf.PingPong(t, 0.5f) + 0.5f;
            _waitText.color = new Color(1, 1, 1, val);
            yield return null;
            t += Time.deltaTime;
        }
    }
}
