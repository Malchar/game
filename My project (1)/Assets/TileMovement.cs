using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public Vector2Int gridPosition; // Current grid position
    public float tileSize = 1f; // Size of a tile (in world units)
    public LayerMask obstacleLayer;

    private Animator animator;
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Initialize position
        gridPosition = Vector2Int.RoundToInt(transform.position / tileSize);
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving) return; // Prevent input while moving

        // Get input
        Vector2Int input = Vector2Int.zero;
        if (Input.GetKeyDown(KeyCode.W)) input = Vector2Int.up;
        if (Input.GetKeyDown(KeyCode.S)) input = Vector2Int.down;
        if (Input.GetKeyDown(KeyCode.A)) input = Vector2Int.left;
        if (Input.GetKeyDown(KeyCode.D)) input = Vector2Int.right;

        if (input != Vector2Int.zero)
        {
            Vector2 targetGridPosition = gridPosition + input;
            Vector2 targetWorldPosition = new Vector2(targetGridPosition.x * tileSize, targetGridPosition.y * tileSize);

            if (!Physics2D.OverlapCircle(targetWorldPosition, 0.1f, obstacleLayer))
            {
                gridPosition += input;
                targetPosition = new Vector3(gridPosition.x * tileSize, gridPosition.y * tileSize, transform.position.z);
                StartCoroutine(MoveToTarget());
            }
        }
        
        // Set animator parameters
        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveY", input.y);
        animator.SetBool("IsMoving", input != Vector2Int.zero);
    }

    private System.Collections.IEnumerator MoveToTarget()
    {
        isMoving = true;

        while ((transform.position - targetPosition).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; // Snap to target
        isMoving = false;
    }
}

