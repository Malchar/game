using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "new Damage Bolt", menuName = "ScriptableObjects/Moves/DamageBolt", order = 1)]
public class DamageBolt : Move
{
    [field: SerializeField]
    public string Name {get; set; }
    [field: SerializeField]
    public string Description {get; set; }
    [field: SerializeField]
    public ElementVector Damage {get; set; }
    [field: SerializeField]
    public int NumTargets {get; set; }
    [field: SerializeField]
    public string PlaceholderStatusEffects;

    public override string GetName()
    {
        return Name;
    }
    public override string GetDescription()
    {
        return Description;
    }

    public override void Invoke(Battler user, Battler[] targets)
    {
        throw new NotImplementedException();
    }
}
