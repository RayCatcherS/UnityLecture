using UnityEngine;

public class RacketController : MonoBehaviour
{
    private enum RacketType
    {
        Player,
        AI
    }

    [SerializeField] private RacketType type;
    [SerializeField] private float speed;
    [SerializeField] private GameObject ball;
    

    private Rigidbody2D rb;
    private Vector2 racketMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {

        if (type == RacketType.Player) {
            PlayerBehaviour();
        } else
        {
            AIBehaviour();
        }
        

        rb.linearVelocity = racketMove * speed;
    }

    private void PlayerBehaviour()
    {
        float yAxis = Input.GetAxisRaw("Vertical");

        racketMove = new Vector2(0, yAxis);
    }

    private void AIBehaviour()
    {
        if (ball.transform.position.y > transform.position.y + .5f) {
            racketMove = new Vector2(0, 1);
        } else if(ball.transform.position.y < transform.position.y - .5f)
        {
            racketMove = new Vector2(0, -1);
        } else
        {
            racketMove = Vector2.zero;
        }
    }
}
