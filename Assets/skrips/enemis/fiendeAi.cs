using UnityEngine;

public class FiendeAi : MonoBehaviour
{
    [SerializeField] private float circleRadius = 2f;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask Player;
    [SerializeField] private LayerMask notEnemy;



    void Update()
    {
        // hitar spelarens relativa riktning
        Vector2 playerDir = (player.transform.position - gameObject.transform.position).normalized;

        // Kollar allt inom cirkeln runt fienden
        Collider2D hit = Physics2D.OverlapCircle(transform.position, circleRadius, Player);

        if (hit != null)
        {
            if ( hit.name == "Player")
            {

                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

                RaycastHit2D hit2D = Physics2D.Raycast(gameObject.transform.position, playerDir, distanceToPlayer, notEnemy);

                if (hit2D != null && hit2D.collider.name == "Player")
                {
                    
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