using UnityEngine;

public enum BallType
{
    Voll,
    Halb,
    Acht
}

public class BallData : MonoBehaviour
{
    public int number;
    public BallType type;
}
