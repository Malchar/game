using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    // this is used to hold a battle for testing
    public ScriptedBattle testBattle;

    public Battler[] friendlyBattlers;
    public Battler[] enemyBattlers;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("battle system starting");
        // the start method probably doens't need to do anything.
        // the game controller should call the battleSystem.startBattle(battlers)
    }

    // Update is called once per frame
    void Update()
    {
        // this should probably delegate to the battlers and the generated effects,
        // which is what would play their animations.
    }

    /* Routine
    1. play intro battle cinematics. no controls
            a battle object has the following fields
                list of levelled monsters (monster, level)
                spoils (xp?, items, recruitments)
                reference to background art asset
                intro battle cinematic override (if different from default)
            battlers are initialized using a PC or a monster template
                for each party member, create battler using that party member as PC template
                as with each levelled monster, create battler using that.
            setup UI to show starting values.
            setup the screen to show the background art, battlers, and UI
            delay until battle cinematic is completed, which may include dialog boxes.
    2. calculate turn order. no controls
            maintain a list of battlers for turn order
                fixed length, will depend on UI design
                allow duplicate references to the same battler since someone can take multiple turns
            pop the first battler from list
    3. select move from active battler's move list.
    4. select targets from among all battlers using move's preferences.
    5. play move animation, update members. no controls.
            move animations can include dialog boxes, battle cinematics, etc.
    6. (repeat from "calculate turn order").
    7. play outro battle cinematics. display spoils. press to confirm.
    */
}
