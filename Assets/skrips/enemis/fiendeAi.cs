using UnityEngine;

public class FiendeAi : MonoBehaviour
{
    [SerializeField] private float circleRadius = 2f;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask Player;
    [SerializeField] private LayerMask notEnemy;
    [SerializeField] private int attackRange = 2;
    [SerializeField] private int momentSped = 2;
    [SerializeField] private float damage = 20f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       

        // Kollar allt inom cirkeln runt fienden
        Collider2D hit = Physics2D.OverlapCircle(transform.position, circleRadius, Player);

        if (hit != null)
        {
            if ( hit.name == "Player")
            {
                // avstndet
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

                // hitar spelarens relativa riktning
                Vector2 playerDir = (player.transform.position - gameObject.transform.position).normalized;

                RaycastHit2D hit2D = Physics2D.Raycast(gameObject.transform.position, playerDir, distanceToPlayer, notEnemy);

                if (hit2D != null && hit2D.collider.name == "Player")
                {
                    if (distanceToPlayer <= attackRange)
                    {
                        rb.linearVelocity = playerDir * 0;
                       float playerhelth = player.GetComponent<movment>().health;
                       player.GetComponent<movment>().health =  playerhelth - damage;
                    }
                    else
                    {
                        rb.linearVelocity = playerDir * momentSped;  
                    }
                }
                
            }
        }
            

        
    }

    // Ritar BARA EN cirkel i Scene-vyn
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
}