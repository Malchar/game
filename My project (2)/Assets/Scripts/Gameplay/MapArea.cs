using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArea : MonoBehaviour
{
    [SerializeField] List<PartyMember> wildEnemies;

    public PartyMember GetRandomWildEnemy(){
        var enemy = wildEnemies[Random.Range(0, wildEnemies.Count)];
        enemy.Init();
        return enemy;
    }
}
