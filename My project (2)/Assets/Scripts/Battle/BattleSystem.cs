using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { Start, ActionSelection, MoveSelection, PerformMove, Busy, BattleOver }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;

    [SerializeField] JobBase testJobBase;
    [SerializeField] int testLevel;
    [SerializeField] string testName;


    [SerializeField] BattleUnit enemyUnit;

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
        enemyUnit.Setup(enemy);

        dialogBox.SetMoveNames(playerUnit.PartyMember.Moves);

        yield return StartCoroutine(dialogBox.TypeDialog($"A wild {enemyUnit.PartyMember.JobBase.name} appeared."));

        ActionSelection();
    }

    void BattleOver(bool won){
        state = BattleState.BattleOver;
        OnBattleOver(won);
    }

    void ActionSelection(){
        state = BattleState.ActionSelection;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }

    void MoveSelection(){
        state = BattleState.MoveSelection;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PlayerMove(){
        state = BattleState.PerformMove;
        var move = playerUnit.PartyMember.Moves[currentMove];
        Debug.Log(move.Description);
        yield return RunMove(playerUnit, enemyUnit, move);
        if (state == BattleState.PerformMove){
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove(){
        state = BattleState.PerformMove;
        var move = enemyUnit.PartyMember.GetRandomMove();
        yield return RunMove(enemyUnit, playerUnit, move);
        if (state == BattleState.PerformMove){
            ActionSelection();
        }
    }

    IEnumerator RunMove(BattleUnit sourceUnit, BattleUnit targetUnit, IMove move){
        yield return dialogBox.TypeDialog($"{sourceUnit.PartyMember.Name} used {move.Name}");

        // animations
        sourceUnit.PlayAttackAnimation();
        yield return new WaitForSeconds(1f);
        targetUnit.PlayHitAnimation();

        var damageDetails = targetUnit.PartyMember.TakeDamage(move, sourceUnit.PartyMember);
        yield return targetUnit.Hud.UpdateHP();
        yield return ShowDamageDetails(damageDetails);

        if (damageDetails.Fainted){
            targetUnit.PlayFaintAnimation();
            yield return dialogBox.TypeDialog($"{targetUnit.PartyMember.Name} fainted");

            yield return new WaitForSeconds(2f);
            CheckForBattleOver(targetUnit);
        }
    }

    void CheckForBattleOver(BattleUnit faintedUnit){
        if (faintedUnit.IsPlayerUnit){
            BattleOver(false);
        } else {
            BattleOver(true);
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
        if (state == BattleState.ActionSelection) {
            HandleActionSelection();
        } else if (state == BattleState.MoveSelection) {
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
                MoveSelection();
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
            StartCoroutine(PlayerMove());
        }
    }

    internal static void DealDamage(BattleUnit user, ElementVector damageAmount, BattleUnit target)
    {
        
    }

    internal static void Heal(BattleUnit user,ElementVector healAmount, BattleUnit target)
    {
        throw new NotImplementedException();
    }
}
