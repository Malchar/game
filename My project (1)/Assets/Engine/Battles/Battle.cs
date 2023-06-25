using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Serializable]
public class Battle : IBattleable
{
    [field: SerializeField]
    public ProtoBattler[] ProtoBattlers {get; set; }
    [field: SerializeField]
    public Sprite Background {get; set; }
    [field: SerializeField]
    public int MoneyReward {get; set; }

    public Battle(ProtoBattler[] protoBattlers, Sprite background, int moneyReward)
    {
        ProtoBattlers = protoBattlers;
        Background = background;
        MoneyReward = moneyReward;
    }

    public Battle GetBattle() {
        return this;
    }
}
