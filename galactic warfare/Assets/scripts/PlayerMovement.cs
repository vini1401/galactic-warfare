using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 input;

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector2 movement = input.normalized * speed * Time.fixedDeltaTime;
        transform.Translate(movement);
    }
}
