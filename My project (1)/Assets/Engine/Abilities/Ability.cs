using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Ability : ScriptableObject, IInvocable, ISelectable
{
    [field: SerializeField]
    public string Description;
    [field: SerializeField]
    public int NumTargets;
    public string GetDescription()
    {
        return Description;
    }
    public string GetName(){
        return this.name;
    }
    public abstract void Invoke(Battler user, Battler[] targets);

    public int GetNumTargets(){
        return NumTargets;
    }
}
