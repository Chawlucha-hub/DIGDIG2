using UnityEngine;
using System.Collections;
public class FiendeAi : MonoBehaviour
{
    [SerializeField] private float circleRadius = 2f;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask Player;
    [SerializeField] private LayerMask notEnemy;
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private int momentSped = 2;
    [SerializeField] private float damage = 20f;
    [SerializeField] public float health = 100f;

    private bool conterstated = false;
    private bool rethtplayer = false;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       if (health<= 0f)
        {
            gameObject.SetActive(false);
        }

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
                    if ((rethtplayer == true) && (distanceToPlayer <= attackRange))
                    {
                        rethtplayer = true;
                        rb.linearVelocity = Vector2.zero;

                            if (conterstated == false)
                            {
                                conterstated = true;
                                float playerhelth = player.GetComponent<movment>().health;
                                player.GetComponent<movment>().health = playerhelth - damage;
                                StartCoroutine(ExampleCoroutine());
                            }
                        
                        
                      
                    } else if(distanceToPlayer <= attackRange -1)
                    {
                        rb.linearVelocity = Vector2.zero;
                        rethtplayer = true;

                        if (conterstated == false)
                        {
                            conterstated = true;
                            float playerhelth = player.GetComponent<movment>().health;
                            player.GetComponent<movment>().health = playerhelth - damage;
                            StartCoroutine(ExampleCoroutine());
                        }
                    }
                    else
                    {
                        rb.linearVelocity = playerDir * momentSped;
                        rethtplayer = false;
                    }
                }
                
            }
        }
            

        
    }
    IEnumerator ExampleCoroutine()
    {

        yield return new WaitForSeconds(1);

        conterstated = false;
    }

    // Ritar BARA EN cirkel i Scene-vyn
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange -1);
    }
}