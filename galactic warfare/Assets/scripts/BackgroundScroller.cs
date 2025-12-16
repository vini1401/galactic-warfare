using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 2f;
    public float resetPositionX;
    public float startPositionX;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x <= resetPositionX)
        {
            transform.position = new Vector2(startPositionX, transform.position.y);
        }
    }
}
