using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IInvocable
{
    public void Invoke(Battler user, Battler[] targets);
}
