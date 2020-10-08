
using UnityEngine;

public class GameValues : Singleton<GameValues>
{
    public float x_boundary;
    public float dropHeight;
    public Vector3 startPosition;
    [Range(0.1f,5)]
    public float dropPeriod;
}
