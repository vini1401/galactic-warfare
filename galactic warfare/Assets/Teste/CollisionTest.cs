using UnityEngine;

public class CollisionTest : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("COLIDIU COM: " + collision.gameObject.name);
    }
}
