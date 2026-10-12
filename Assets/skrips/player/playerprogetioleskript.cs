using UnityEngine;

public class playerprogetioleskript : MonoBehaviour
{
    Vector2 direktion;
    Rigidbody2D rb;
    GameObject finde;
    [SerializeField] private int speed = 7;
    [SerializeField] private float damage = 5f;

    private void Awake()
    {
        direktion = playeratakskript.playerDirection;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.linearVelocity = direktion * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       finde = collision.gameObject;
        if (finde.tag != "Player") {
            if (finde.layer == 6)
            {
                finde.GetComponent<FiendeAi>().health -= damage;
            }
            Destroy(gameObject);
        }
       
    }
}
