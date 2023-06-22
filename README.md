# game roadmap

battle system, state pattern
1. introduction - play start-of-battle cinematics. initialize all members. player has no controls.
2. calculate turn order. player has no controls.
3. choose move. if player is moving, select from active member's move list.
4. choose targets. if player is moving, select from active move's valid targets. (generally, all targets are valid, but the active move will default to enemy or friendly, self or other, or target a whole side.)
5. display animation, update members. player has no controls.
6. (repeat from "calculate turn order".)
7. outro - play end-of-battle cinematics. display and collect spoils of battle. player press to confirm battle results screen.

