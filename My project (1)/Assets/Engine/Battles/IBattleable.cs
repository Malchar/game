using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
This interface represents every object that is a battle. 
It is needed to allow ScriptedBattle to have multiple inheritance
*/
public interface IBattleable
{
    public Battle GetBattle();
}
