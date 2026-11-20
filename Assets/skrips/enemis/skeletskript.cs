using UnityEngine;
using System.Collections;
public class skeletskript : MonoBehaviour
{
    [SerializeField] private float circleRadius = 8f;
    [SerializeField] private float cirkelTwoClose = 3f;
    [SerializeField] private GameObject player;
    [SerializeField] private LayerMask Player;
    [SerializeField] private LayerMask notEnemy;
    [SerializeField] public float attackRange = 6f;
    [SerializeField] private int momentSped = 2;
    [SerializeField] public static float damage = 20f;
    
    [SerializeField] public Animator animator;
    

    private bool conterstated = false;
    private bool rethtplayer = false;

    private Rigidbody2D rb;

    // chekdistens 
    [SerializeField] bool Run;
    [SerializeField] bool attakrech;
    [SerializeField] bool attak;
    [SerializeField] bool jagar;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {




        Collider2D hit = Physics2D.OverlapCircle(transform.position, circleRadius, Player);
       
        if (hit != null)
        {
            if (hit.name == "Player")
            {
                // avstndet
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

                // hitar spelarens relativa riktning
                Vector2 playerDir = (player.transform.position - gameObject.transform.position).normalized;


                RaycastHit2D hit2D = Physics2D.Raycast(gameObject.transform.position, playerDir, distanceToPlayer, notEnemy);

                float angle = Mathf.Atan2(playerDir.y, playerDir.x) * Mathf.Rad2Deg;
                
                
                if (hit2D.collider.name == "Player")
                {
                    gameObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                    if (distanceToPlayer < cirkelTwoClose)
                    {
                        Run = true;
                        rb.linearVelocity = -playerDir * momentSped;
                        StartCoroutine(ExampleCoroutine());
                    }
                    else if ((rethtplayer == true) && (distanceToPlayer <= attackRange))
                    {
                        attakrech = true;
                        //animator.SetBool("går", false);
                        rethtplayer = true;
                        rb.linearVelocity = Vector2.zero;

                        if (conterstated == false)
                        {
                            conterstated = true;

                            StartCoroutine(ExampleCoroutine());
                        }



                    }
                    else if (distanceToPlayer <= attackRange - 2)
                    {
                        attak = true;
                        rb.linearVelocity = Vector2.zero;
                        rethtplayer = true;
                        animator.SetBool("går", false);
                        if (conterstated == false)
                        {
                            conterstated = true;
                            
                            StartCoroutine(ExampleCoroutine());
                        }
                    }
                    else
                    {
                        jagar = true;
                        rb.linearVelocity = playerDir * momentSped;
                        rethtplayer = false;
                        //animator.SetBool("går", true);
                    }
                }

            }
        }

        IEnumerator ExampleCoroutine()
        {
            //animator.SetBool("går", false);
            //yield return null;
            //animator.Play("", 0, 0f);
            //yield return null;
            //while (!animator.GetCurrentAnimatorStateInfo(0).IsName(""))
           // {

                //yield return null;
           // }
            //while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            //{

            //    yield return null;
            //}
            //animator.Play("idol", 0, 0f);


            yield return new WaitForSeconds(0.5f);

            conterstated = false;
        }

    }
}
