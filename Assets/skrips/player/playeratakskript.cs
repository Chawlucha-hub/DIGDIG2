using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playeratakskript : MonoBehaviour
{
    [SerializeField] private static float damage = 10f;

    public static float attackRadius = 2;
    public static Vector2 curentPos;
    public static Vector2 playerDirection;

    [SerializeField] public static LayerMask finede;

    public static List<GameObject> fiender = new List<GameObject>();

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
            finede
        );

        foreach (Collider2D hit in hits)
        {
            GameObject fiende = hit.gameObject;

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
        for(int i = 0; i < fiender.Count; i++)
        {
            fiender[i].GetComponent<FiendeAi>().health -= damage;
        }
    }
}