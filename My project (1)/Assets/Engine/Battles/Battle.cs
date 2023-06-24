using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Serializable]
public class Battle : IBattleable
{
    [field: SerializeField]
    public Monster[] Monsters {get; set; }
    [field: SerializeField]
    public Sprite Background {get; set; }
    [field: SerializeField]
    public int MoneyReward {get; set; }

    public Battle(Monster[] monsters, Sprite background, int moneyReward)
    {
        Monsters = monsters;
        Background = background;
        MoneyReward = moneyReward;
    }

    public Battle GetBattle() {
        return this;
    }
}
