using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    const float _playerSpeed = 10f;
    [SerializeField] private PlayerModel _playerModel;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private GameObject _scoreGmo;
    [SerializeField] private TextMeshProUGUI _scoreText;
    private Rigidbody rb;

    private HeathBarController _heathBarController;
    private CallBack _dieCallBack;

    public void SetCallBack(CallBack callBack)
    {
        _dieCallBack = callBack;
    }

    private void OnEnable()
    {
        Reset();
        rb = GetComponent<Rigidbody>();
        _playerModel = new PlayerModel(100, GameManager.Instance.GetMaxScore());
        _healthBar.SetActive(true);
        _scoreGmo.SetActive(true);
        _heathBarController = _healthBar.GetComponent<HeathBarController>();
        HealthVisualator();
    }

    private void OnDisable()
    {
        _healthBar?.SetActive(false);
        _scoreGmo.SetActive(false);
        _dieCallBack = null;
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0, 0);
        rb.velocity = movement * _playerSpeed;

    }

    public void ChangeHitPoint(int damege)
    {
        _playerModel.ChangeHitPoint(damege);
        if(_playerModel.GetHitPoint() == 0)
        {
            Die();
        }
        HealthVisualator();
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

    private void HealthVisualator()
    {
        _heathBarController.UpdateSliderValue(_playerModel.GetHitPoint());
    }

    public void Reset()
    {
        gameObject.transform.position = GameValues.Instance.startPosition;
    }
}
