
using UnityEngine;

public class PlayerModel
{
    private int _hitPoint;
    private int _maxHitPoint;
    private int _score;

    public PlayerModel(int maxHitPoint, int maxScore)
    {
        _maxHitPoint = maxHitPoint;
        _hitPoint = maxHitPoint;
    }

    public int GetHitPoint()
    {
        return _hitPoint;
    }

    public void ChangeHitPoint(int hpChange)
    {
        if (hpChange < 0)
        {
            _hitPoint += hpChange;
            if (_hitPoint <= 0)
            {
                _hitPoint = 0;
            }
        }
        else
        {
            if (_hitPoint < _maxHitPoint)
            {
                _hitPoint += hpChange;
                if (_hitPoint > _maxHitPoint)
                {
                    _hitPoint = _maxHitPoint;

                }
            }
        }
    }

    public int GetScore()
    {
        return _score;
    }

    public void ChangeScore(int score)
    {
        _score += score;
    }
}
