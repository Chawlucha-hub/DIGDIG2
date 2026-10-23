using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class playeratakskript : MonoBehaviour
{
    [SerializeField] private static float damage = 10f;

    public static float attackRadius = 2;
    public static Vector2 curentPos;
    public static Vector2 playerDirection;
    public static bool isBlocing = false;
    public static Vector2 staticplayerdiretion;
    public static Animator Sanimator;
    public Animator animator;
    public static bool slår = false;
    public static GameObject playerObjekt;


    [SerializeField] public LayerMask finede;
    private static LayerMask enemyLayer;

    public static List<GameObject> fiender = new List<GameObject>();

    private void Awake()
    {
        playerObjekt = gameObject;
        enemyLayer = finede;
        animator = gameObject.GetComponent<movment>().animator;
        Sanimator = gameObject.GetComponent<movment>().animator;
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

        slår = true;
        playerObjekt.GetComponent<playeratakskript>().StartSlagAnimation();

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
        
        if (isBlocing == true)
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
                player.GetComponent<playeratakskript>().StartaBlockAnimation();
            }
        }
        else
        {
            player.GetComponent<movment>().health -= damage;
           Sanimator.SetBool("Blockattak", false);
        }

       
    }

    private Coroutine blockCoroutine;


    public void StartaBlockAnimation()
    {
        if (blockCoroutine != null)
            return;

        blockCoroutine = StartCoroutine(VantaPaAnimation());
    }

    IEnumerator VantaPaAnimation()
    {
       
            animator.SetBool("Blockattak", true);

            // Vänta tills block-state börjar.
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("block"))
                yield return null;

            // Vänta tills animationen spelats klart.
            while (animator.GetCurrentAnimatorStateInfo(0).IsName("block") &&
                   animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                yield return null;

            animator.SetBool("Blockattak", false);
            blockCoroutine = null;

    }

    private Coroutine slagCoroutine;

    public void StartSlagAnimation()
    {
        if (slagCoroutine != null)
            return;

        slagCoroutine = StartCoroutine(VantaPaSlagAnimation());
    }

    IEnumerator VantaPaSlagAnimation()
    {
        animator.SetBool("attacking", true);

        // Vänta tills Animator har uppdaterat parametern.
        yield return null;

        // Starta attack-state från början.
        animator.Play("attacking", 0, 0f);

        // Vänta en bildruta så att Animator hinner byta state.
        yield return null;

        // Vänta tills state har spelats klart en gång.
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("attacking") &&
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }

        animator.SetBool("attacking", false);

        slår = false;
        slagCoroutine = null;
    }

}





