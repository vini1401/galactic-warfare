using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int life;
    public float speed;
    public int damage;
    public int scoreValue;
}
