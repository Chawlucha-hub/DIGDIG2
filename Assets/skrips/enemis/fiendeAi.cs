using UnityEngine;

public class FiendeAi : MonoBehaviour
{
    [SerializeField] private float circleRadius = 2f;
   



    void Update()
    {

        // Kollar allt inom cirkeln runt fienden
        Collider2D hit = Physics2D.OverlapCircle(transform.position, circleRadius);

        if (hit != null)
        {
            if (hit.name == "Player")
            {
                // RaycastHit2D hit2D = Physics2D.GetContactColliders();
            }

            Debug.Log("Träffade: " + hit.name);
        }
    }

    // Ritar BARA EN cirkel i Scene-vyn
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }
}