using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

[CreateAssetMenu(fileName = "new Scripted Battle", menuName = "ScriptableObjects/ScriptedBattle", order = 3)]
public class ScriptedBattle : ScriptableObject, IBattleable
{
    [field: SerializeField]
    public Battle Battle {get; set; }

    public Battle GetBattle(){
        return Battle;
    }
}
