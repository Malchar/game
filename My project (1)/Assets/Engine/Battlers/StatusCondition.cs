using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public abstract class StatusCondition
{
    public string DisplayName {get; set; }
    public abstract void OnTurnStart();
}
