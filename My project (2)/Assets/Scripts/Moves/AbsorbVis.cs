using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbsorbVis : AbstractMove
{
    public override List<TargetType> TargetTypes => new() {TargetType.Single, TargetType.Single};

    public override string Description => "Target creature loses 10 life. Target creature gains 10 life.";

    private ElementVector damageAmount = new(10, ElementType.Wind);
    private ElementVector healAmount = new(10, ElementType.Heart);

    public override void PerformMove(BattleUnit user, List<List<BattleUnit>> listOfTargets)
    {
        // display animation

        var target1 = listOfTargets[0][0];
        BattleSystem.DealDamage(user, damageAmount, target1);

        var target2 = listOfTargets[1][0];
        BattleSystem.Heal(user, healAmount, target2);

        // update GUI (update during animation if feasible)
    }
    /**
    *   AbstractMove needs to become an interface on the PerformMove method
    *   need to make this generic and scriptable object
    *   can probably make it highly parameterized with various moveTypes
    *   like 1st target, do damage, do statusEffect
    *       2nd target, do heal, do statBuff
    *   where each moveType is parameterized.
    *   don't go too crazy yet. this will be sufficiently expandible.
    */
}
