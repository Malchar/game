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
    private TurnQueue turnQueue; // tracks battler initiative
    private Battler currentBattler;
    private int currentAbilityIndex;
    private Ability selectedAbility;
    private List<Battler> allBattlers; // used for target selection
    private List<Battler> selectableBattlers;
    private int currentTargetIndex;
    private List<Battler> selectedTargets;

    // handles initializations
    void Start() {
        // retrieve battlers and bind to battlerUI
        List<Battler> playerBattlers = BattleData.Instance.PlayerBattlers;
        List<Battler> enemyBattlers = BattleData.Instance.EnemyBattlers;
        battleCanvas.Initialize(playerBattlers, enemyBattlers);

        // set up reference list of all battlers. used for target selection and turn queue
        allBattlers = new List<Battler>(playerBattlers);
        allBattlers.AddRange(enemyBattlers);

        // subscribe to events
        foreach (Battler battler in allBattlers)
        {
            battler.OnAgilityChanged += OnBattlerAgilityChanged;
        }

        // set up turn queue
        // if we want to have surprise rounds, then the battle manager should
        // initialize each battler's Initiative value before passing to the turnQueue.
        turnQueue.Initialize(allBattlers);

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
        currentBattler = turnQueue.ExtractNextBattler();
        UpdateTurnQueueUI();
        if (currentBattler.IsEnemy) {
            StartEnemyTurn();
        } else {
            StartPlayerAbilitySelection();
        }
    }

    // updates the turn queue and the battle canvas display
    private void UpdateTurnQueueUI(){
        List<Battler> futureBattlers = turnQueue.PeekUpcomingBattlers(battleCanvas.GetNumTurnIcons());
        battleCanvas.UpdateTurnIcons(futureBattlers);
    }

    public void OnBattlerAgilityChanged(Battler battler)
    {
        UpdateTurnQueueUI();
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
        selectableBattlers = new List<Battler>(allBattlers);
        
        // remove battlers that cannot be targetted by the selected ability
        selectableBattlers = selectableBattlers.Where(battler => battler.CanBeTargeted(selectedAbility)).ToList();

        //handling for when there are no valid targets
        if (selectableBattlers.Count == 0) {
            Debug.Log("no valid targets for that ability");
            StartPlayerAbilitySelection();
        } else {
            // Highlight the initial target
            currentTargetIndex = 0;
            battleCanvas.SetShowTargetCursor(selectableBattlers[currentTargetIndex], true);
        }
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
        battleCanvas.SetShowTargetCursor(selectableBattlers[currentTargetIndex], false);
        currentTargetIndex = (currentTargetIndex + direction + selectableBattlers.Count) % selectableBattlers.Count;
        battleCanvas.SetShowTargetCursor(selectableBattlers[currentTargetIndex], true);
    }

    private void ConfirmTarget()
    {
        // TODO also remove the target from the list of selectable, so it cannot be picked again
        selectedTargets.Add(selectableBattlers[currentTargetIndex]);
        Debug.Log($"Target selected: {selectableBattlers[currentTargetIndex].GetName()}");

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
        Debug.Log("current turn resolved, starting next turn..");
        StartNextTurn();
    }

    void UpdateResolveTurn()
    {
        // update method during resolve turn
        // this will run each frame
    }

    public void StartEnemyTurn(){
        currentState = BattleState.EnemyTurn;
        List<Ability> abilities = currentBattler.GetAbilities().ToList();
        while (abilities.Count > 0) {
            // determine ability
            selectedAbility = abilities[UnityEngine.Random.Range(0, abilities.Count)];
            abilities.Remove(selectedAbility);

            // determine targets
            selectedTargets = new();
            selectableBattlers = new List<Battler>(playerBattlers); // assume ability is hostile
            selectableBattlers = selectableBattlers.Where(battler => battler.CanBeTargeted(selectedAbility)).ToList();

            // pick a different ability if there aren't enough targets
            if (selectableBattlers.Count < selectedAbility.GetNumTargets()) {
                continue;
            } else {
                while (selectedTargets.Count < selectedAbility.GetNumTargets()) {
                    Battler selectedBattler = selectableBattlers[UnityEngine.Random.Range(0, selectableBattlers.Count)];
                    selectableBattlers.Remove(selectedBattler);
                    selectedTargets.Add(selectedBattler);
                }
                StartResolveTurn();
                return;
            }
        }
        Debug.Log("enemy has no usable moves, starting next turn..");
        StartNextTurn();
    }

    void UpdateEnemyTurn()
    {
        // update method during resolve turn
        // this will run each frame. can probably re-use UpdateResolveTurn
    }

    void UpdateHandleVictory()
    {
        // Handle victory logic
        // runs each frame
    }

    void UpdateHandleDefeat()
    {
        // Handle defeat logic
        // runs each frame
    }

    public void EndBattle()
    {
        // unsubscribe from events
        foreach (Battler battler in allBattlers)
        {
            battler.OnAgilityChanged -= OnBattlerAgilityChanged;
        }

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
