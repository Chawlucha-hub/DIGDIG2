using UnityEngine;

public class playeratakskript : MonoBehaviour
{
    [SerializeField] private float damage = 10f;

    public movment movmentScript;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = movmentScript.inputActions;
    }

    void Update()
    {
        Vector2 attack = inputActions.Player.Attack.ReadValue<Vector2>();

        if (attack.x < 0)
        {
            Debug.Log("Q trycktes!");
        }

        if (attack.x > 0)
        {
            Debug.Log("E trycktes!");
        }
    }
}
