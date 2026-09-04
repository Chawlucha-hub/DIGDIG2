using UnityEngine;
using UnityEngine.InputSystem;

public class movment : MonoBehaviour
{
    private PlayerInputActions inputActions;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject camara;

    
    

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
        // WADS movent
        Vector2 moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        transform.Translate(new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime);

        // musposition så man kan tita åt alla holl
        Vector2 mouseScreenPos = Mouse.current.position.ReadValue();

        Vector3 mouseInput = new Vector3(mouseScreenPos.x, mouseScreenPos.y, -Camera.main.transform.position.z);

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseInput);
        mouseWorldPos.z = 0f;

        // Räkna ut riktningen från spelaren till musen
        Vector2 direction = mouseWorldPos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);




        

        camara.transform.position = new Vector3 (gameObject.transform.position.x,gameObject.transform.position.y,-10f);

        camara.transform.rotation = Quaternion.identity;
    }
}
