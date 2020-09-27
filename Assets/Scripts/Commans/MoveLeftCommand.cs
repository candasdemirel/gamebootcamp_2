using UnityEngine;

public class MoveLeftCommand : ICommand
{
    float _speed;
    Rigidbody _rb;
    float _lag;

    public MoveLeftCommand(Rigidbody rb, float speed, float lag)
    {
        _rb = rb;
        _speed = speed;
        _lag = lag;
    }

    public void Execute()
    {
        _rb.position = _rb.position + new Vector3(-_speed, 0, 0);
    }

    public float GetLag()
    {
        return _lag;
    }

    public void Undo()
    {
        _rb.position = _rb.position + new Vector3(_speed, 0, 0);
    }
}
