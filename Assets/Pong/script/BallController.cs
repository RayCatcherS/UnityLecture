using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 6f;
    [SerializeField] private const float speedIncrease = 0.25f;
    private int hitCounter = 0; // contatore di collisioni con racchette
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.linearVelocity = new Vector2(1, 0) * (initialSpeed + (hitCounter * speedIncrease));
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = Vector2.ClampMagnitude(
            rb.linearVelocity, // vettore su cui applicare il clamp
            initialSpeed + (hitCounter * speedIncrease) // valore massimo
        );
    }

    

    private void CalculateTrajectory(Vector2 racketCenterPosition)
    {
        Vector2 ballPosition = transform.position;

        float newXDirection = 0;
        float newYDirection = 0;

        if(transform.position.x > 0)
        {
            newXDirection = -1;
        } else
        {
            newXDirection = 1;
        }

        newYDirection = ballPosition.y - racketCenterPosition.y;


        if(Mathf.Abs(newYDirection) < .1f)
        {
            if(float.IsNegative(newYDirection))
            {
                newYDirection = .15f;
            } else
            {
                newYDirection = -.15f;
            }
        }

        rb.linearVelocity = new Vector2(
            newXDirection,
            newYDirection
            ) * (initialSpeed + (hitCounter * speedIncrease)
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "racketPlayer" || collision.gameObject.name == "racketAI")
        {
            hitCounter = hitCounter + 1;
            CalculateTrajectory(collision.transform.gameObject.transform.position);
        }
    }
}
