using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask solidObjectsLayer;
    public bool isMoving;
    private Vector2 input;

    private Animator animator;
    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!isMoving){
            input.x = Random.Range(-1, 2);
            input.y = Random.Range(-1, 2);
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

    }

    private bool IsWalkable(Vector3 targetPos){
       return Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer) == null;
    }
}
