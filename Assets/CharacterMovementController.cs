using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Animator")]
    public Animator characterAnimator;

    private Vector3 previousPosition;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        previousPosition = transform.position;

        if (characterAnimator == null)
        {
            Debug.LogWarning("Animator not assigned on CharacterMovementController.");
        }
    }

    void Update()
    {
        // Get input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Calculate movement direction
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Smooth rotation towards movement direction
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Move the character
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }

        // Toggle "Run" animation based on position change
        bool isMoving = Vector3.Distance(transform.position, previousPosition) > 0.001f;

        if (characterAnimator != null)
        {
            characterAnimator.SetBool("Run", isMoving);
        }

        previousPosition = transform.position;
    }
}