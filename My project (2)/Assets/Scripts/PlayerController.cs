using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public LayerMask grassLayer;

    public event Action OnEncountered;

    public bool isMoving;
    private Vector2 input;

    private Animator animator;
    private void Awake(){
        animator = GetComponent<Animator>();
    }

    public void HandleUpdate() {
        if (!isMoving){
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            if (input != Vector2.zero){
                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos)) {
                    StartCoroutine(Move(targetPos));
                }
                
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos){
        isMoving = true;
        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;

        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos){
       return Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) == null;
    }

    private void CheckForEncounters(){
        if (Physics2D.OverlapCircle(transform.position, 0.1f, grassLayer) != null){
            // player walked on grass
            if (UnityEngine.Random.Range(1, 101) <= 10){
                Debug.Log("Encountered a wild pokemon");
                animator.SetBool("isMoving", false);
                OnEncountered();
            }
        }
    }
}
