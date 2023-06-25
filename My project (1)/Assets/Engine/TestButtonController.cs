using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonController : MonoBehaviour
{

    public Button moveButton;
    public Button attackButton;
    public Button damageButton;
    public GameObject characterToAnimate;
    public bool IsMoving = false;


    [Header("Game Objects")]
    private Animator _animator;
    private void FindGameObjects(){
        _animator = characterToAnimate.GetComponent<Animator>();
    }
    private void NullChecking(){
        if (_animator == null)
            Debug.LogError("animator is null. was it assigned?");
    }
    // Start is called before the first frame update
    void Start()
    {
        FindGameObjects();
        NullChecking();
        moveButton.onClick.AddListener(Move);
        attackButton.onClick.AddListener(Attack);
        damageButton.onClick.AddListener(TakeDamage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move() {
        IsMoving = !IsMoving;
        _animator.SetBool("IsMoving", IsMoving);
    }

    public void Attack() {
        _animator.Play("Attack");
    }

    public void TakeDamage() {
        _animator.Play("TakeDamage");
    }


}
