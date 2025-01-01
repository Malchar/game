using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleCanvas battleCanvas; // Reference to the Canvas
    private BattleState currentState;
    private List<Battler> playerBattlers;
    private List<Battler> enemyBattlers;
    //TODO make some kind of list to track initiative
    private List<Battler> initiativeList;
    private Battler currentBattler;
    private int currentAbilityIndex;
    private Ability selectedAbility;
    private List<Battler> allBattlers; // used for target selection
    private int currentTargetIndex;
    private List<Battler> selectedTargets;

    // handles initializations
    void Start() {
        // retrieve battlers and bind to battlerUI
        List<Battler> playerBattlers = BattleData.Instance.PlayerBattlers;
        List<Battler> enemyBattlers = BattleData.Instance.EnemyBattlers;
        battleCanvas.Initialize(playerBattlers, enemyBattlers);

        // set battle state
        currentState = BattleState.Start;
        StartBattle();
    }

    void Update() {
        switch (currentState) {
            case BattleState.PlayerAbility:
                UpdatePlayerAbilitySelection();
                break;
            case BattleState.PlayerTarget:
                UpdatePlayerTargetSelection();
                break;
            case BattleState.EnemyTurn:
                UpdateEnemyTurn();
                break;
            case BattleState.ResolveTurn:
                UpdateResolveTurn();
                break;
            case BattleState.Victory:
                UpdateHandleVictory();
                break;
            case BattleState.Defeat:
                UpdateHandleDefeat();
                break;
        }
    }

    // handles one-time events at start of battle
    void StartBattle() {
        Debug.Log("Battle has started!");
        StartNextTurn();
    }

    /*
     starts a new turn of the battle by setting the currentBattler 
     and then moving to the next stage.
     */
    void StartNextTurn(){
        CalculateInitiative();
        currentBattler = initiativeList[0];
        if (currentBattler.IsEnemy) {
            StartEnemyTurn();
        } else {
            StartPlayerAbilitySelection();
        }
    }

    // sets current battler and the initiative list UI
    private void CalculateInitiative(){
        /*
        TODO draw the initiative list in the UI. i don't think we even need to store it
        in the battle manager since it won't actually be used for anything.
        */
    }

    public void StartPlayerAbilitySelection(){
        currentState = BattleState.PlayerAbility;

        Ability[] abilities = currentBattler.GetAbilities();
        currentAbilityIndex = 0;
        battleCanvas.ShowAbilitySelectorBox(currentBattler);
        battleCanvas.SetShowAbilityCursor(abilities[currentAbilityIndex], true);
    }

    void UpdatePlayerAbilitySelection()
    {
        // Logic for player to select a target
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveAbilityCursor(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveAbilityCursor(-1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveAbilityCursor(-2);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveAbilityCursor(2);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmAbility();
        }
    }

    private void MoveAbilityCursor(int direction)
    {
        Ability[] abilities = currentBattler.GetAbilities();
        battleCanvas.SetShowAbilityCursor(abilities[currentAbilityIndex], false);
        currentAbilityIndex = (currentAbilityIndex + direction + abilities.Length) % abilities.Length;
        battleCanvas.SetShowAbilityCursor(abilities[currentAbilityIndex], true);
    }

    private void ConfirmAbility()
    {
        Ability[] abilities = currentBattler.GetAbilities();
        selectedAbility = abilities[currentAbilityIndex];
        Debug.Log($"Ability selected: {selectedAbility.GetName()}");
        battleCanvas.HideAbilitySelectorBox();
        StartPlayerTargetSelection();
    }

    public void StartPlayerTargetSelection()
    {
        currentState = BattleState.PlayerTarget;

        // initialize list of selected targets
        selectedTargets = new();

        // create list of eligible targets
        // TODO build the list in a different order depending on the selected ability
        // TODO change the targetting mode entirely depending on the selected ability
        allBattlers = new List<Battler>(playerBattlers);
        allBattlers.AddRange(enemyBattlers);
        // remove battlers that cannot be targetted by the selected ability
        allBattlers = allBattlers.Where(battler => battler.CanBeTargeted(selectedAbility)).ToList();

        // TODO add handling here for when there are no valid targets
        if (allBattlers.Count == 0) {
            StartPlayerAbilitySelection();
            return;
        }
        // Highlight the initial target
        currentTargetIndex = 0;
        battleCanvas.SetShowTargetCursor(allBattlers[currentTargetIndex], true);
    }

    void UpdatePlayerTargetSelection()
    {
        // Logic for player to select a target
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveTargetCursor(1);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveTargetCursor(-1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)
         || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTargetCursor(Math.Min(playerBattlers.Count, enemyBattlers.Count));
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmTarget();
        }
        // TODO handle back button
        
    }

    private void MoveTargetCursor(int direction)
    {
        battleCanvas.SetShowTargetCursor(allBattlers[currentTargetIndex], false);
        currentTargetIndex = (currentTargetIndex + direction + allBattlers.Count) % allBattlers.Count;
        battleCanvas.SetShowTargetCursor(allBattlers[currentTargetIndex], true);
    }

    private void ConfirmTarget()
    {
        selectedTargets.Add(allBattlers[currentTargetIndex]);
        Debug.Log($"Target selected: {allBattlers[currentTargetIndex].GetName()}");

        // check if done selecting targets
        if (selectedAbility.GetNumTargets() == selectedTargets.Count) {
            // Proceed with ability execution
            battleCanvas.ClearAllTargetCursors();
            StartResolveTurn();
        } else {
            // TODO add some kind of indicator that a target was selected.
        }
    }

    // Logic for resolving the effects of abilities
    void StartResolveTurn(){
        currentState = BattleState.ResolveTurn;
        
        // TODO RESUME HERE
        selectedAbility.Invoke(currentBattler, selectedTargets.ToArray());
        // if invoke wants to draw stuff on the battle,
        // then the battle manager should pass it a reference to the canvas that it can use.

        // maybe invoke needs to be an IEnumerator, or whatever the async thing is.
        // wait until the Invoke finishes its animations??

        // check if either side is defeated
        // Transition to Victory, Defeat, or next turn
        StartNextTurn();
    }

    void UpdateResolveTurn()
    {
        // update method during resolve turn
    }

    public void StartEnemyTurn(){
        currentState = BattleState.EnemyTurn;

        // TODO
    }
    void UpdateEnemyTurn()
    {
        // update method during resolve turn
    }

    void UpdateHandleVictory()
    {
        // Handle victory logic
    }

    void UpdateHandleDefeat()
    {
        // Handle defeat logic
    }

    public void EndBattle()
    {
        // Notify GameManager that the battle is over
        GameManager.Instance.EndBattle();
    }

    private enum BattleState {
        Start,         // Battle setup phase
        PlayerAbility,    // Player is selecting an ability
        PlayerTarget,  // Player is selecting a target
        EnemyTurn,     // Enemy is taking its turn
        ResolveTurn,   // Processing the effects of abilities
        Victory,       // Player wins
        Defeat         // Player loses
    }
}
