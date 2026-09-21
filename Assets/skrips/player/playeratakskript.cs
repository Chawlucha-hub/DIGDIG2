using UnityEngine;
using UnityEngine.InputSystem;
using static Unity.Collections.AllocatorManager;

public class playeratakskript : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
   

    public static void Attack()
    {
        Debug.Log("attack");
        

    }
}
