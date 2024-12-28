using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
The BattleCanvas will be responsible for handling all of the graphics during battles.
It is driven by the BattleManager.
*/
public class BattleCanvas : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Transform[] playerBattlerSlots; // Preset Transforms for player battlers
    [SerializeField] private Transform[] enemyBattlerSlots;  // Preset Transforms for enemy battlers
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text battleText;
    [SerializeField] private Transform actionPanel;
    [SerializeField] private GameObject battlerUIPrefab;
    private List<Battler> playerBattlers;
    private List<Battler> enemyBattlers;

    // sample method that would be used by the BattleManager
    public void SetBackground(Sprite image) {
        background.sprite = image;
    }

    /*
    handles all tasks necessary to set up a battle
    */
    public void Initialize(List<Battler> playerBattlers, List<Battler> enemyBattlers) {
        InitializeBattlers(playerBattlers, enemyBattlers);
    }

    private void InitializeBattlers(List<Battler> playerBattlers, List<Battler> enemyBattlers) {
        this.playerBattlers = playerBattlers;
        this.enemyBattlers = enemyBattlers;
        SetupBattlersUI(playerBattlers, playerBattlerSlots);
        SetupBattlersUI(enemyBattlers, enemyBattlerSlots);
    }

    /*
    creates battlerUIprefabs and battlerUI objects for each battler
    */
    private void SetupBattlersUI(List<Battler> battlers, Transform[] battlerSlots) {
        for (int i = 0; i < battlers.Count; ++i) {
            if (i >= battlerSlots.Length) 
            {
                Debug.LogWarning($"Not enough slots for battlers. Skipping battler {battlers[i].GetName()}");
                continue;
            }

            GameObject battlerUIObject = Instantiate(battlerUIPrefab, battlerSlots[i]);

            BattlerUI battlerUI = battlerUIObject.GetComponent<BattlerUI>();
            if (battlerUI != null)
            {
                /*
                TODO store all battlerUI's in some kind of data structure.
                they will need to be used for the target selection interface.
                the battlerUI holds a reference to the battler, so the selection
                can get the target from there.
                */
                battlerUI.Setup(battlers[i]);
            }
            else
            {
                Debug.LogError("BattlerUI prefab is missing a BattlerUI component!");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
