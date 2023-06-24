using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Fireball : Move
{
    public int Damage {get; set; } = 5;
    public Battler User {get; set; }
    public Battler[] Targets {get; set; }

    // different types of moves will have different number of targets
    // this constructor should probably be the only one for all moves
    public Fireball(Battler user, Battler[] targets) {
        this.User = user;
        this.Targets = targets;
    }

    public void DoStuff() {
        // calculate actual damage output using User
        // deal damage to targets, which will apply their resistances
    }
}
