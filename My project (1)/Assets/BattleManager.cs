using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private BattleCanvas battleCanvas; // Reference to the Canvas
    private List<Battler> playerBattlers;
    private List<Battler> enemyBattlers;
    private List<Battler> allBattlers;
    private BattleState currentState;
    private Battler currentBattler;
    private Ability selectedAbility;
    private int currentTargetIndex;
    private List<Battler> selectedTargets;

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
                HandlePlayerAbility();
                break;
            case BattleState.PlayerTarget:
                HandlePlayerTarget();
                break;
            case BattleState.EnemyTurn:
                HandleEnemyTurn();
                break;
            case BattleState.ResolveTurn:
                ResolveTurn();
                break;
            case BattleState.Victory:
                HandleVictory();
                break;
            case BattleState.Defeat:
                HandleDefeat();
                break;
        }
    }

    void StartBattle() {
        Debug.Log("Battle has started!");
        StartNextTurn();
    }

    void StartNextTurn(){
        CalculateInitiative();
        if (currentBattler.IsEnemy) {
            StartEnemyTurn();
        } else {
            StartAbilitySelection();
        }
    }

    // sets current battler and the initiative list UI
    private void CalculateInitiative(){
        /*
        TODO draw the initiative list in the UI. i don't think we even need to store it
        in the battle manager since it won't actually be used for anything.
        */

        // TODO set the current battler
    }

    public void StartEnemyTurn(){
        currentState = BattleState.EnemyTurn;

        // TODO
    }

    public void StartAbilitySelection(){
        currentState = BattleState.PlayerAbility;

        currentBattler.GetAbilities();
        // TODO build this out in the same way as the target selection
    }

    void HandlePlayerAbility()
    {
        Debug.Log("Player is selecting an ability.");
        // Logic for player to select a target
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveAbilityCursor(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveAbilityCursor(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmAbility();
        }
    }

    private void MoveAbilityCursor(int direction)
    {
        //TODO retrofit to abilities
        battleCanvas.SetShowCursor(allBattlers[currentTargetIndex], false);
        currentTargetIndex = (currentTargetIndex + direction + allBattlers.Count) % allBattlers.Count;
        battleCanvas.SetShowCursor(allBattlers[currentTargetIndex], true);
    }

    private void ConfirmAbility()
    {
        // TODO retrofit to abilities
        selectedTargets.Add(allBattlers[currentTargetIndex]);
        Debug.Log($"Target selected: {allBattlers[currentTargetIndex].GetName()}");

        // check if done selecting targets
        if (selectedAbility.GetNumTargets() == selectedTargets.Count) {
            // Proceed with ability execution
            battleCanvas.ClearAllCursors();
            currentState = BattleState.ResolveTurn;
        } else {
            // TODO add some kind of indicator that a target was selected.
        }
    }

    public void StartTargetSelection()
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
            StartAbilitySelection();
            return;
        }
        // Highlight the initial target
        currentTargetIndex = 0;
        battleCanvas.SetShowCursor(allBattlers[currentTargetIndex], true);
    }

    void HandlePlayerTarget()
    {
        Debug.Log("Player is selecting a target.");
        // Logic for player to select a target
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTargetCursor(1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTargetCursor(-1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            ConfirmTarget();
        }
        // TODO handle back button
        
    }

    private void MoveTargetCursor(int direction)
    {
        battleCanvas.SetShowCursor(allBattlers[currentTargetIndex], false);
        currentTargetIndex = (currentTargetIndex + direction + allBattlers.Count) % allBattlers.Count;
        battleCanvas.SetShowCursor(allBattlers[currentTargetIndex], true);
    }

    private void ConfirmTarget()
    {
        selectedTargets.Add(allBattlers[currentTargetIndex]);
        Debug.Log($"Target selected: {allBattlers[currentTargetIndex].GetName()}");

        // check if done selecting targets
        if (selectedAbility.GetNumTargets() == selectedTargets.Count) {
            // Proceed with ability execution
            battleCanvas.ClearAllCursors();
            currentState = BattleState.ResolveTurn;
        } else {
            // TODO add some kind of indicator that a target was selected.
        }
    }

    void HandleEnemyTurn()
    {
        Debug.Log("Enemy is taking its turn.");
        // Logic for enemy's turn
        currentState = BattleState.ResolveTurn;
    }

    void ResolveTurn()
    {
        Debug.Log("Resolving turn effects.");
        // Logic for resolving the effects of abilities
        // Transition to Victory, Defeat, or next turn
    }

    // a sample method. note that battler.TakeDamage signals the battlerUI update.
    public void ApplyDamageToEnemyBattler(int battlerIndex, int damage)
    {
        if (battlerIndex < 0 || battlerIndex >= enemyBattlers.Count) return;

        Battler battler = enemyBattlers[battlerIndex];
        battler.TakeDamage(damage);
    }

    void HandleVictory()
    {
        Debug.Log("Player wins!");
        // Handle victory logic
    }

    void HandleDefeat()
    {
        Debug.Log("Player loses!");
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
