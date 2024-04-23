using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHud playerHud;

    [SerializeField] JobBase testJobBase;
    [SerializeField] int testLevel;
    [SerializeField] string testName;


    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud enemyHud;

    [SerializeField] JobBase testEnemyJobBase;
    [SerializeField] int testEnemyLevel;
    [SerializeField] string testEnemyName;


    [SerializeField] BattleDialogBox dialogBox;
    

    BattleState state;
    int currentAction;
    int currentMove;

    public event Action<bool> OnBattleOver;

    Party playerParty;
    PartyMember enemy;

    public void StartBattle(Party playerParty, PartyMember enemy){
        this.playerParty = playerParty;
        this.enemy = enemy;
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle(){
        playerUnit.Setup(playerParty.GetHealthyMember());
        playerHud.SetData(playerUnit.PartyMember);

        enemyUnit.Setup(enemy);
        enemyHud.SetData(enemyUnit.PartyMember);

        dialogBox.SetMoveNames(playerUnit.PartyMember.Moves);

        yield return StartCoroutine(dialogBox.TypeDialog($"A wild {enemyUnit.PartyMember.JobBase.name} appeared."));

        PlayerAction();
    }

    void PlayerAction(){
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove(){
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove(){
        state = BattleState.Busy;
        var move = playerUnit.PartyMember.Moves[currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.PartyMember.Name} used {move.name}");

        // animations
        playerUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);
        enemyUnit.PlayHitAnimation();

        var damageDetails = enemyUnit.PartyMember.TakeDamage(move, playerUnit.PartyMember);
        yield return enemyHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted){
            enemyUnit.PlayFaintAnimation();
            yield return dialogBox.TypeDialog($"{enemyUnit.PartyMember.Name} fainted");

            // player won the battle
            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
        } else {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove(){
        state = BattleState.EnemyMove;

        var move = enemyUnit.PartyMember.GetRandomMove();
        yield return dialogBox.TypeDialog($"{enemyUnit.PartyMember.Name} used {move.name}");

        // animations
        enemyUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);
        playerUnit.PlayHitAnimation();

        var damageDetails = playerUnit.PartyMember.TakeDamage(move, enemyUnit.PartyMember);
        yield return playerHud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted){
            playerUnit.PlayFaintAnimation();
            yield return dialogBox.TypeDialog($"{playerUnit.PartyMember.Name} fainted");

            
            yield return new WaitForSeconds(2f);

            var nextMember = playerParty.GetHealthyMember();
            if (nextMember != null){
                playerUnit.Setup(nextMember);
                playerHud.SetData(playerUnit.PartyMember);
                dialogBox.SetMoveNames(playerUnit.PartyMember.Moves);
                yield return dialogBox.TypeDialog($"Go {playerUnit.PartyMember.Name}!");
                PlayerAction();
            } else {
                // player lost the battle
                OnBattleOver(false);
            }
            
        } else {
            PlayerAction();
        }
    }

    IEnumerator ShowDamageDetails(DamageDetails damageDetails){
        if (damageDetails.Critical > 1f){
            yield return dialogBox.TypeDialog("A critical hit!");
        }
        if (damageDetails.Type > 1f){
            yield return dialogBox.TypeDialog("It's super effective!");
        } else if (damageDetails.Type < 1f) {
            yield return dialogBox.TypeDialog("It's not very effective!");
        }

    }

    public void HandleUpdate(){
        if (state == BattleState.PlayerAction) {
            HandleActionSelection();
        } else if (state == BattleState.PlayerMove) {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection(){
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            if (currentAction < 1){
                ++currentAction;
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (currentAction > 0){
                --currentAction;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z)){
            if (currentAction == 0){
                // fight
                PlayerMove();
            } else if (currentAction == 1){
                // run
            }
        }
    }

    void HandleMoveSelection(){
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            if (currentMove < playerUnit.PartyMember.Moves.Count - 1){
                ++currentMove;
            }
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)){
            if (currentMove > 0){
                --currentMove;
            }
        } else if (Input.GetKeyDown(KeyCode.DownArrow)){
            if (currentMove < playerUnit.PartyMember.Moves.Count - 2){
                currentMove += 2;
            }
        } else if (Input.GetKeyDown(KeyCode.UpArrow)){
            if (currentMove > 1){
                currentMove -= 2;
            }
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.PartyMember.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z)){
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }
    }

    internal static void DealDamage(BattleUnit user, ElementVector damageAmount, BattleUnit target)
    {
        throw new NotImplementedException();
    }

    internal static void Heal(BattleUnit user,ElementVector healAmount, BattleUnit target)
    {
        throw new NotImplementedException();
    }
}
