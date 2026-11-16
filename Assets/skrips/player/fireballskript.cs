using System.Collections;
using UnityEngine;

public class fireballskript : MonoBehaviour
{
    Vector2 direktion;
    Rigidbody2D rb;
    GameObject finde;
    [SerializeField] private int speed = 7;
     private float damage = 75f;
    [SerializeField] public Animator animator;
    private bool destrying = false;
    private Vector2 postition;
    private float explotionRaduse = 1.5f;


    private void Awake()
    {
        direktion = playeratakskript.playerDirection;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        postition = gameObject.transform.position;
        if (destrying == false)
        {
            rb.linearVelocity = direktion * speed;
        }
        else { rb.linearVelocity = direktion * 0; }

    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            postition,
            explotionRaduse);
        finde = collision.gameObject;
        if (finde.tag != "Player")
        {
            destrying = true;
            foreach (Collider2D hit in hits)
            {
                try
                {
                    hit.GetComponent<FiendeAi>().health -= damage;
                }
                catch
                {
                    
                }
                try
                {
                    hit.GetComponent<movment>().health -= damage;
                }
                catch
                {

                }
            }
            

            animator.Play("fierbalslut", 0, 0f);
            yield return null;
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("fierbalslut"))
            {

                yield return null;
            }
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {

                yield return null;
            }

            Destroy(gameObject);
        }
       
    }
}
