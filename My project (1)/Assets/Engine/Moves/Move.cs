using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Move : ScriptableObject, IInvocable, ISelectable
{
    [field: SerializeField]
    public string Description;
    public string GetDescription()
    {
        return Description;
    }
    public string GetName(){
        return this.name;
    }
    public abstract void Invoke(Battler user, Battler[] targets);
}
