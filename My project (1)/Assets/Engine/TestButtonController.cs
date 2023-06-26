using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonController : MonoBehaviour
{
    // stage 1
    public Button moveButton;
    public Button attackButton;
    public Button damageButton;
    public GameObject characterToAnimate;
    public bool IsMoving = false;

    // stage 2
    public Button buttonConfirm;
    public Dropdown dropdownMoves;
    public GameObject enemyCharacter;
    public Battler battlerFriendly;
    public Battler battlerEnemy;

    [Header("Game Objects")]
    private Animator animatorFriendly;
    private Animator animatorEnemy;
    private void FindGameObjects(){
        animatorFriendly = characterToAnimate.GetComponent<Animator>();
        animatorEnemy = enemyCharacter.GetComponent<Animator>();

        battlerFriendly = characterToAnimate.GetComponent<Battler>();
        battlerEnemy = enemyCharacter.GetComponent<Battler>();
    }
    private void NullChecking(){
        if (animatorFriendly == null)
            Debug.LogError("friendly animator is null. was it assigned?");
        if (animatorEnemy == null)
            Debug.LogError("enemy animator is null. was it assigned?");

        if (battlerFriendly == null)
            Debug.LogError("battler friendly is null. was it assigned?");
        if (battlerEnemy == null)
            Debug.LogError("battler enemy is null. was it assigned?");
    }
    // Start is called before the first frame update
    void Start()
    {
        FindGameObjects();
        NullChecking();
        moveButton.onClick.AddListener(Move);
        attackButton.onClick.AddListener(Attack);
        damageButton.onClick.AddListener(TakeDamage);

        buttonConfirm.onClick.AddListener(Confirm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move() {
        IsMoving = !IsMoving;
        animatorFriendly.SetBool("IsMoving", IsMoving);
    }

    public void Attack() {
        animatorFriendly.Play("Attack");
    }

    public void TakeDamage() {
        animatorFriendly.Play("TakeDamage");
    }

    public void Confirm() {
        int indexSelected = dropdownMoves.value;
        Move moveSelected = battlerFriendly.GetMoves()[indexSelected];
        Debug.Log("index " + indexSelected + ", move " + moveSelected.GetName());
    }


}
