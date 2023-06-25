using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class Move : MonoBehaviour
{
    public Battler User {get; set; }
    public Battler[] Targets {get; set; }

    // different types of moves will have different number of targets
    // this constructor should probably be the only one for all moves
    public Move(Battler user, Battler[] targets) {
        this.User = user;
        this.Targets = targets;
    }

    public abstract void DoMove();
}
