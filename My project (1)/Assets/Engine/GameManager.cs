using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
In a setup with a dedicated battle scene, the BattleManager typically exists only in the battle scene, while the GameManager exists across all scenes (non-battle and battle). Here's a breakdown of the responsibilities and structure for the scenes and the GameObjects in them:

Non-Battle Scene
The non-battle scene handles exploration, menus, or overworld mechanics.

Key GameObjects:
GameManager (Persistent):

Exists across all scenes (marked with DontDestroyOnLoad).
Manages high-level game state (e.g., switching between exploration and battle, saving/loading, etc.).
Holds references to global systems like player data, settings, etc.
PlayerController:

Controls the player's movement and interactions in the overworld.
Environment:

Contains world elements like terrain, NPCs, items, etc.
UI:

Displays exploration-related UI, such as inventory, quests, etc.
Battle Scene
The battle scene is focused on combat mechanics.

Key GameObjects:
BattleManager:

Exists only in the battle scene.
Manages the flow of the battle (e.g., player/enemy turns, animations, results).
Initializes using data passed from the GameManager.
Canvas:

Displays battle-specific UI (e.g., battlers' health, abilities menu, target selection).
Battler Objects:

Instantiated at runtime based on the data (player and enemy battlers).
Represent the battlers on the battlefield (e.g., their models or sprites).
Environment:

Represents the battlefield's background (e.g., terrain, decorations).
*/
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    public void StartBattle(List<Battler> playerBattlers, List<Battler> enemyBattlers)
    {
        // Store data and load the battle scene
        BattleData.Instance.SetData(playerBattlers, enemyBattlers);
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
    }

    public void EndBattle()
    {
        // Transition back to the previous non-battle scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("OverworldScene");
    }
}

