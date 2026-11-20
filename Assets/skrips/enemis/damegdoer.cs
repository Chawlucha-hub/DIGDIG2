using UnityEngine;
using System.Collections;


public class damegdoer : MonoBehaviour
{
    [SerializeField] Animator animator;
    private float helthcheker;
    [SerializeField] public float health = 100f;

    private void Start()
    {
        helthcheker = health;
    }

    void Update()
    {
        if (health <= 0f)
        {
            gameObject.SetActive(false);
        }
        else if (health < helthcheker)
        {
            helthcheker = health;
            StartCoroutine(damech());
        }
    }
    IEnumerator damech()
    {
        animator.SetBool("går", false);
        yield return null;
        animator.Play("tarskada", 0, 0f);
        yield return null;
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("tarskada"))
        {

            yield return null;
        }
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {

            yield return null;
        }
        animator.Play("idol", 0, 0f);
    }
}
