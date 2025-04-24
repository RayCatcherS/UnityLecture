using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class characterMove : MonoBehaviour
{
    private Rigidbody2D rb;

    private int coins = 0;

    [SerializeField] private float playerSpeed;
    [SerializeField] private GameObject coinRef;
    [SerializeField] private GameObject coinRefParticle;
    [SerializeField] private Text scoreTxt;

    Vector2 playerVelocity = Vector2.zero;
    void Start()
    {
        SpawnCoin();
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    private void PlayerJoinBehavior()
    {
        float yAxis = Input.GetAxisRaw("Vertical");
        float xAxis = Input.GetAxisRaw("Horizontal");

        playerVelocity = new Vector2(
            xAxis,
            yAxis
            );
    }

    public void SpawnCoin()
    {
        Instantiate(coinRef, 
            new Vector3(
                Random.Range(-7, 7),
                Random.Range(-3, 3)
                )
            , Quaternion.identity);
    }
    public void SpawnParticle(Vector2 pos)
    {
        Instantiate(coinRefParticle,
            pos
            , Quaternion.identity);
    }

    void FixedUpdate() {

        PlayerJoinBehavior();
        rb.linearVelocity = playerVelocity * playerSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "coin")
        {
            coins ++;
            scoreTxt.text = "Punteggio: " + coins;
            Destroy(collision.gameObject);

            SpawnParticle(collision.transform.position);
            SpawnCoin();
        }
        
    }
}
