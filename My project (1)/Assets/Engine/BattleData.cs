using System.Collections;
using System.Collections.Generic;

public class BattleData
{
    public static BattleData Instance { get; } = new BattleData();

    public List<Battler> PlayerBattlers { get; private set; }
    public List<Battler> EnemyBattlers { get; private set; }

    public void SetData(List<Battler> playerBattlers, List<Battler> enemyBattlers)
    {
        PlayerBattlers = playerBattlers;
        EnemyBattlers = enemyBattlers;
    }

    public void Clear()
    {
        PlayerBattlers = null;
        EnemyBattlers = null;
    }
}
