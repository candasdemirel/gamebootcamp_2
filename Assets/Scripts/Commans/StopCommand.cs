using UnityEngine;

public class StopCommand : ICommand
{
    private Rigidbody _rb;
    float _lag;

    public StopCommand(Rigidbody rb, float lag)
    {
        _rb = rb;
        _lag = lag;
    }

    public void Execute()
    {
        _rb.velocity = Vector3.zero;
    }

    public float GetLag()
    {
        return _lag;
    }

    public void Undo()
    {
        _rb.velocity = Vector3.zero;
    }
}
