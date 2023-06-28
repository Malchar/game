using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestButtonController : MonoBehaviour
{
    // stage 1
    public Button moveButton;
    public Button attackButton;
    public Button damageButton;
    public GameObject characterToAnimate;
    private bool IsMoving = false;

    // stage 2
    public Button buttonConfirm;
    //public ScrollView scrollView;
    public FillScrollView scrollViewController;
    public GameObject enemyCharacter;
    private Battler battlerFriendly;
    private Battler battlerEnemy;

    [Header("Game Objects")]
    private Animator animatorFriendly;
    private Animator animatorEnemy;
    private void FindGameObjects(){
        // TODO -- change these to be like the scrollViewController
        animatorFriendly = characterToAnimate.GetComponent<Animator>();
        animatorEnemy = enemyCharacter.GetComponent<Animator>();
        //scrollViewController = scrollView.GetComponent<FillScrollView>;

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
        
        // fill scrollview
        DamageBolt fireBolt = new();
        fireBolt.name = "Fire Bolt";
        DamageBolt frostBolt = new();
        frostBolt.name = "Frost Bolt";
        DamageBolt lightningBolt = new();
        lightningBolt.name = "Lightning Bolt";
        Move[] moves = {fireBolt, frostBolt, fireBolt, lightningBolt};
        scrollViewController.SetContent(moves);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move() {
        IsMoving = !IsMoving;
        animatorFriendly.SetBool("IsMoving", IsMoving);
        Debug.Log("clicked move");
    }

    public void Attack() {
        animatorFriendly.Play("Attack");
        Debug.Log("clicked attack");
    }

    public void TakeDamage() {
        animatorFriendly.Play("TakeDamage");
        Debug.Log("clicked take damage");
    }

    public void Confirm() {
        Debug.Log("move ...");
    }


}
