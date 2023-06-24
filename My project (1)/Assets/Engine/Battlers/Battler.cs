using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler : MonoBehaviour
{
    // the battler is a temporary object which only exists during battle.
    // necessary fields are initialized from the held ProtoBattler, but
    // grainularity is preserved in case things change during battle.
    public ProtoBattler ProtoBattler {get; set; }
    public int HP {get; set; }
    public StatusCondition[] StatusConditions {get; set; }
    public int Initiative {get; set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
