using System.Collections.Generic;
using System.Linq;

namespace botters
{
    public class InitData
    {
        public readonly int Team;
        public readonly List<Spawn> Spawns;
        public readonly List<Bush> Bushes;
        public readonly List<Item> Items;
        public AttackDirection AttackDirection => Team == 0 ? AttackDirection.Right : AttackDirection.Left;

        public InitData(int team, List<Entity> staticObjects, List<Item> items)
        {
            Team = team;
            Spawns = staticObjects.OfType<Spawn>().ToList();
            Bushes = staticObjects.OfType<Bush>().ToList();
            Items = items;
        }
    }
}