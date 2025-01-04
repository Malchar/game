using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TurnQueue
{
    private List<Battler> battlers = new();

    public void Initialize(List<Battler> battlers)
    {
        this.battlers = new List<Battler>(battlers);
        foreach (Battler battler in this.battlers) {
            battler.Initiative = 0;
        }
    }

    public Battler ExtractNextBattler()
    {
        // Find the Battler with the highest TurnPoints (and break ties with Speed)
        Battler next = battlers.OrderByDescending(b => b.Initiative)
                               .ThenByDescending(b => b.GetStats().Agility)
                               .First();

        // Update TurnPoints for all Battlers and record the total increase.
        int totalIncrease = 0;
        foreach (var battler in battlers)
        {
            totalIncrease += battler.IncreaseInitiative();
        }

        // Decrease the next battler by the same amount to make the system zero-sum.
        next.Initiative -= totalIncrease;

        return next;
    }

    public List<Battler> PeekUpcomingBattlers(int count)
    {
        // Simulate upcoming turns based on current TurnPoints and Speed
        var simulatedQueue = new List<Battler>(battlers);

        List<Battler> upcomingBattlers = new List<Battler>();
        for (int i = 0; i < count; i++)
        {
            simulatedQueue = simulatedQueue.OrderByDescending(b => b.Initiative)
                                           .ThenByDescending(b => b.GetStats().Agility)
                                           .ToList();

            Battler next = simulatedQueue.First();
            upcomingBattlers.Add(next);

            // Update TurnPoints for all Battlers and record the total increase.
            int totalIncrease = 0;
            foreach (var battler in simulatedQueue)
            {
                totalIncrease += battler.IncreaseInitiative();
            }

            // Decrease the next battler by the same amount to make the system zero-sum.
            next.Initiative -= totalIncrease;
        }

        return upcomingBattlers;
    }
}
