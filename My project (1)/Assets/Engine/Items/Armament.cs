using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "new Armament", menuName = "ScriptableObjects/Items/Armament", order = 1)]
public class Armament : Item
{
    [field: SerializeField]
    public HandType HandType {get; set; }
    [field: SerializeField]
    public bool IsWeapon {get; set; }
}
