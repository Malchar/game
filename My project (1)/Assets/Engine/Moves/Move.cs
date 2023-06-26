using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Move : ScriptableObject, IInvocable, ISelectable
{
    public abstract string GetDescription();
    public abstract string GetName();
    public abstract void Invoke(Battler user, Battler[] targets);
}
