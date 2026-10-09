using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playeratakskript : MonoBehaviour
{
    [SerializeField] private static float damage = 10f;

    public static float attackRadius = 2;
    public static Vector2 curentPos;
    public static Vector2 playerDirection;
    public static bool isBlocing = false;
    
    
    [SerializeField] public LayerMask finede;
    private static LayerMask enemyLayer;

    public static List<GameObject> fiender = new List<GameObject>();

    private void Awake()
    {
        enemyLayer = finede;
    }

    private void Update()
    {
        curentPos = transform.position;
        playerDirection = transform.up;
    }

    public static void Attack()
    {
        Debug.Log("attack");

        fiender.Clear();

        Collider2D[] hits = Physics2D.OverlapCircleAll(
            curentPos,
            attackRadius,
            enemyLayer
        );

        foreach (Collider2D hit in hits)
        {
            GameObject fiende = hit.gameObject;

            Debug.Log("hit " + fiende);

            Vector2 riktningTillFiende =
                ((Vector2)fiende.transform.position - curentPos).normalized;

            float vinkel = Vector2.Angle(
                playerDirection,
                riktningTillFiende
            );

            if (vinkel <= 60f)
            {
                fiender.Add(fiende);
            }
        }


        for (int i = 0; i < fiender.Count; i++)
        {
            FiendeAi fiendeAi = fiender[i].GetComponent<FiendeAi>();

            if (fiendeAi != null)
            {
                fiendeAi.health -= damage;
            }
            else
            {
                Debug.LogError("Fienden saknar FiendeAi: " + fiender[i].name);
            }
        }

    }
    

    public static void attackDeDisider(float damage, GameObject fiende, GameObject player)
    {
        if(isBlocing == true)
        {
            Vector2 riktnigTillSlag = (curentPos - (Vector2)fiende.transform.position).normalized;
            float vikelAvSlag = Vector2.Angle(playerDirection, riktnigTillSlag);
            if (vikelAvSlag <= 120)
            {
                player.GetComponent<movment>().health -= damage;
            }
            else
            {
                Debug.Log("blokat");
            }
        }
        else
        {
            player.GetComponent<movment>().health -= damage;
        }
       
    }
}