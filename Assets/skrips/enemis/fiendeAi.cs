using UnityEngine;
using System.Collections;
public class FiendeAi : MonoBehaviour
{
    [SerializeField] private float circleRadius = 2f;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask Player;
    [SerializeField] private LayerMask notEnemy;
    [SerializeField] public float attackRange = 2.5f;
    [SerializeField] private int momentSped = 2;
    [SerializeField] public static float damage = 20f;
    [SerializeField] public float health = 100f;
    [SerializeField] public Animator animator;

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

                float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
                gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

                if (hit2D.collider.name == "Player")
                {
                    if ((rethtplayer == true) && (distanceToPlayer <= attackRange))
                    {
                        animator.SetBool("går", false);
                        rethtplayer = true;
                        rb.linearVelocity = Vector2.zero;

                            if (conterstated == false)
                            {
                                conterstated = true;

                                playeratakskript.attackDeDisider(damage, gameObject, player);

                            
                                StartCoroutine(ExampleCoroutine());
                            }
                        
                        
                      
                    } else if(distanceToPlayer <= attackRange -1)
                    {
                        rb.linearVelocity = Vector2.zero;
                        rethtplayer = true;
                        animator.SetBool("går", false);
                        if (conterstated == false)
                        {
                            conterstated = true;
                            playeratakskript.attackDeDisider(damage, gameObject, player);
                            StartCoroutine(ExampleCoroutine());
                        }
                    }
                    else
                    {
                        rb.linearVelocity = playerDir * momentSped;
                        rethtplayer = false;
                        animator.SetBool("går", true);
                    }
                }
                
            }
        }
        else { animator.SetBool("går", false); }
            

        
    }
    IEnumerator ExampleCoroutine()
    {
        animator.SetBool("går", false);
        yield return null;
        animator.Play("goblinattackaniamtion", 0, 0f);
        yield return null;
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("goblinattackaniamtion"))
        {

            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {

            yield return null;
        }
        animator.Play("idol", 0, 0f);
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