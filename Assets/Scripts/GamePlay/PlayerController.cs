using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour ,ISubject
{
    const float _playerSpeed = 0.02f;
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private GameObject _scoreGmo;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private Rigidbody rb;
    private bool _isPaused;
    float commandLagTime;


    private CallBack _dieCallBack;

    public void SetCallBack(CallBack callBack)
    {
        _dieCallBack = callBack;
    }

    private void OnEnable()
    {
        EventManager.DamageEventResult += ChangeHitPoint;
        EventManager.ScoreEventResult += ChangeScore;
        EventManager.PauseStateEvent += EventManager_PauseStateEvent;
        //PauseGameState.Subscribe(this, this.gameObject);
        Reset();
        rb = GetComponent<Rigidbody>();
        _playerModel = new PlayerModel(100, GameManager.Instance.GetMaxScore());
        _healthBar.SetActive(true);
        _scoreGmo.SetActive(true);
    }

    private void OnDisable()
    {
        EventManager.DamageEventResult -= ChangeHitPoint;
        EventManager.ScoreEventResult -= ChangeScore;
        EventManager.PauseStateEvent -= EventManager_PauseStateEvent;

        //PauseGameState.Unsubscribe(this, this.gameObject);
        _healthBar?.SetActive(false);
        _scoreGmo.SetActive(false);
        _dieCallBack = null;
    }

    void EventManager_PauseStateEvent(bool isPaused)
    {
        _isPaused = isPaused;
    }


    private void Update()
    {
        if (!_isPaused)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                var command = new MoveLeftCommand(rb, _playerSpeed, commandLagTime);
                command.Execute();
                CommandManager.Instance.AddCommand(command);
                commandLagTime = 0;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                var command = new StopCommand(rb, commandLagTime);
                command.Execute();
                CommandManager.Instance.AddCommand(command);
                commandLagTime = 0;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                var command = new MoveRightCommand(rb, _playerSpeed , commandLagTime);
                command.Execute();
                CommandManager.Instance.AddCommand(command);
                commandLagTime = 0;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                var command = new StopCommand(rb, commandLagTime);
                command.Execute();
                CommandManager.Instance.AddCommand(command);
                commandLagTime = 0;
            }
            commandLagTime += Time.deltaTime;
        }
    }


    public void ChangeHitPoint(int damege)
    {
        _playerModel.ChangeHitPoint(damege);
        if(_playerModel.GetHitPoint() == 0)
        {
            Die();
        }
    }

    public void ChangeScore(int score)
    {
        _playerModel.ChangeScore(score);
        ScoreVisualator();
    }

    private void ScoreVisualator()
    {
        _scoreText.text = _playerModel.GetScore().ToString(); 
    }

    private void Die()
    {
        _dieCallBack();
        var gameManager = GameManager.Instance;
        gameManager.SetMaxScore(_playerModel.GetScore());
        gameManager.SetState(StateType.PreGameState);
    }

    public void Reset()
    {
        gameObject.transform.position = GameValues.Instance.startPosition;
    }

    public void Notify(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
