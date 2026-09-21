using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Collections.AllocatorManager;

public class playeratakskript : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    public static float attackRadius = 2;
    public static Vector2 curentPos;
    [SerializeField] public static LayerMask finede;


    private void Update()
    {
        curentPos = gameObject.transform.position;
    }


    public static void Attack()
    {
        Debug.Log("attack");
        Collider2D hit = Physics2D.OverlapCircle(curentPos, attackRadius, finede);
        if(hit != null)
        {

        }

    }
}
