using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Serializable]
public class Monster : ProtoBattler
{
    // placeholder for the thing that decides which move the monster will use
    [field: SerializeField]
    public UnityEngine.Object AI {get; set; }
}
