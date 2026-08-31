using UnityEngine;

public class movment : MonoBehaviour
{
    private PlayerInputActions inputActions;
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Update()
    {
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime);
    }
}
