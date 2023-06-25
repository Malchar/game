using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Fireball : Move
{
    public int Damage {get; set; } = 5;

    public Fireball(Battler user, Battler[] targets) : base(user, targets)
    {
    }

    public override void DoMove()
    {
        throw new NotImplementedException();
    }

    void Start() {
        // create particles
    }

    void Update() {
        // run state machine
    }
}
