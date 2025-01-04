using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

/*
The BattleCanvas will be responsible for handling all of the graphics during battles.
It is driven by the BattleManager.
*/
public class BattleCanvas : MonoBehaviour
{
    [Header("Battlers")]
    [SerializeField] private Transform[] playerBattlerSlots; // Preset Transforms for player battlers
    [SerializeField] private Transform[] enemyBattlerSlots;  // Preset Transforms for enemy battlers
    [SerializeField] private GameObject battlerUIPrefab;
    [Header("Ability Panel")]
    [SerializeField] private GameObject abilityPanel;
    [SerializeField] private MenuAbility[] menuAbilities;
    [Header("Turn Icons")]
    [SerializeField] private TurnIcon[] turnIcons;
    [Header("Misc.")]
    [SerializeField] private TMP_Text battleText;
    [SerializeField] private Image background;

    // the references to the instantiated battlerUI's
    private Dictionary<Battler, BattlerUI> battlerToUIMap;
    private Dictionary<Ability, MenuAbility> abilityToUIMap;

    public int GetNumTurnIcons() {
        return turnIcons.Length;
    }

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

    public void UpdateTurnIcons(List<Battler> futureBattlers) {
        for(int i = 0; i < futureBattlers.Count; ++i) {
            turnIcons[i].Setup(futureBattlers[i].GetIcon());
        }
    }

    public void ShowAbilitySelectorBox(Battler battler) {
        abilityToUIMap = new();
        Ability[] abilities = battler.GetAbilities();

        for(int i = 0; i < menuAbilities.Length; ++i) {
            MenuAbility menuAbility = menuAbilities[i];
            menuAbility.SetHighlighted(false);

            if (i < abilities.Length) {
                Ability ability = abilities[i];
                menuAbility.SetText(ability.GetName());
                abilityToUIMap.Add(ability, menuAbility);
            } else {
                menuAbility.SetText("");
            }
        }
        abilityPanel.SetActive(true);
    }

    public void HideAbilitySelectorBox() {
        abilityPanel.SetActive(false);
    }

    public void SetShowAbilityCursor(Ability ability, bool isSelected) {
        if (abilityToUIMap.ContainsKey(ability)) {
            MenuAbility menuAbility = abilityToUIMap[ability];
            menuAbility.SetHighlighted(isSelected);
        } else {
            Debug.LogError("set show ability cursor on ability which is not in map. " + ability.GetName());
        }
    }

    public void SetShowTargetCursor(Battler battler, bool isSelected)
    {
        if (battlerToUIMap.ContainsKey(battler))
        {
            BattlerUI battlerUI = battlerToUIMap[battler];
            battlerUI.ShowCursor(isSelected);
        } else {
            Debug.LogError("set show target cursor on battler which is not in map. " + battler.GetName());
        }
    }

    public void ClearAllTargetCursors()
    {
        foreach(BattlerUI battlerUI in battlerToUIMap.Values) {
            battlerUI.ShowCursor(false);
        }
    }
}
