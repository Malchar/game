using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    // the references to the instantiated battlerUI's
    private Dictionary<Battler, BattlerUI> battlerToUIMap;

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
        SetupBattlersUI(playerBattlers, playerBattlerSlots);
        SetupBattlersUI(enemyBattlers, enemyBattlerSlots);
    }

    /*
    creates battlerUIprefabs and battlerUI objects for each battler
    */
    private void SetupBattlersUI(List<Battler> battlers, Transform[] battlerSlots) {
        battlerToUIMap = new Dictionary<Battler, BattlerUI>();
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
                battlerUI.Bind(battlers[i]);
                battlerToUIMap.Add(battlers[i], battlerUI);
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

    public void SetShowCursor(Battler battler, bool isSelected)
    {
        if (battlerToUIMap.ContainsKey(battler))
        {
            BattlerUI battlerUI = battlerToUIMap[battler];
            battlerUI.ShowCursor(isSelected);
        }
    }

    public void ClearAllCursors()
    {
        foreach(BattlerUI battlerUI in battlerToUIMap.Values) {
            battlerUI.ShowCursor(false);
        }
    }
}
