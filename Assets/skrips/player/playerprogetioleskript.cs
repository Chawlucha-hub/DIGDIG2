using System.Collections;
using UnityEngine;

public class playerprogetioleskript : MonoBehaviour
{
    Vector2 direktion;
    Rigidbody2D rb;
    GameObject finde;
    [SerializeField] private int speed = 7;
    [SerializeField] private float damage = 5f;
    [SerializeField]public Animator animator;

    private void Awake()
    {
        
        direktion = playeratakskript.playerDirection;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.linearVelocity = direktion * speed;
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
       finde = collision.gameObject;
        
        if (finde.tag != "Player") {
            if (finde.layer == 6)
            {
                finde.GetComponent<FiendeAi>().health -= damage;
            }
            
            animator.Play("slut",0,0f);
            yield return null;
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }

            Destroy(gameObject);
        }
       
    }
}
