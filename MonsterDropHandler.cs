using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;

namespace Enhanced_Rings
{
    internal static class MonsterDropHandler
    {
        /// <summary>
        /// Harmony Postfix for monsterDrop
        /// </summary>
        public static void MonsterDrop_Postfix(Monster monster, int x, int y, Farmer who)
        {
            try
            {
                if (monster == null || who == null)
                    return;

                bool hasBurglarRingPlus = who.isWearingRing("BurglarRingPlus");
                bool hasBurglarRingPlus2 = who.isWearingRing("BurglarRingPlus2");
                bool hasBurglarRingPlus3 = who.isWearingRing("BurglarRingPlus3");

                if (!hasBurglarRingPlus && !hasBurglarRingPlus2 && !hasBurglarRingPlus3)
                    return;
                int extraRounds = 0;
                var config = ModEntry.GetInstance().Config;
                if (hasBurglarRingPlus3)
                    extraRounds = config.BurglarRingPlus3Rounds;
                else if (hasBurglarRingPlus2)
                    extraRounds = config.BurglarRingPlus2Rounds;
                else if (hasBurglarRingPlus)
                    extraRounds = config.BurglarRingPlusRounds;
                IList<string> objects = monster.objectsToDrop;
                Vector2 playerPosition = Utility.PointToVector2(who.StandingPixel);
                List<Item> extraDrops = monster.getExtraDropItems();
                for (int j = 0; j < extraRounds; j++)
                {
                    if ((hasBurglarRingPlus || hasBurglarRingPlus2 || hasBurglarRingPlus3) && DataLoader.Monsters(Game1.content).TryGetValue(monster.Name, out string? result))
                    {
                        string[] objectsSplit = ArgUtility.SplitBySpace(result.Split('/', StringSplitOptions.None)[6]);
                        for (int i = 0; i < objectsSplit.Length; i += 2)
                        {
                            if (Game1.random.NextDouble() < Convert.ToDouble(objectsSplit[i + 1]))
                            {
                                objects.Add(objectsSplit[i]);
                            }
                        }
                    }
                }
                List<Debris> debrisToAdd = new();
                for (int j = 0; j < objects.Count; j++)
                {
                    string objectToAdd = objects[j];
                    if (objectToAdd != null && objectToAdd.StartsWith('-') && int.TryParse(objectToAdd, out int parsedIndex))
                    {
                        debrisToAdd.Add(monster.ModifyMonsterLoot(new Debris(Math.Abs(parsedIndex), Game1.random.Next(1, 4), new Vector2((float)x, (float)y), playerPosition, 1f)));
                    }
                    else
                    {
                        debrisToAdd.Add(monster.ModifyMonsterLoot(new Debris(objectToAdd, new Vector2((float)x, (float)y), playerPosition)));
                    }
                }
                for (int k = 0; k < extraDrops.Count; k++)
                {
                    debrisToAdd.Add(monster.ModifyMonsterLoot(new Debris(extraDrops[k], new Vector2((float)x, (float)y), playerPosition)));
                }
                for (int j = 0; j < extraRounds; j++)
                {
                    if (hasBurglarRingPlus || hasBurglarRingPlus2 || hasBurglarRingPlus3)
                    {
                        extraDrops = monster.getExtraDropItems();
                        for (int l = 0; l < extraDrops.Count; l++)
                        {
                            Item tmp = extraDrops[l].getOne();
                            tmp.Stack = extraDrops[l].Stack;
                            tmp.HasBeenInInventory = false;
                            debrisToAdd.Add(monster.ModifyMonsterLoot(new Debris(tmp, new Vector2((float)x, (float)y), playerPosition)));
                        }
                    }
                }
                foreach (Debris d in debrisToAdd)
                {
                    monster.currentLocation.debris.Add(d);
                }
            }
            catch (Exception ex)
            {
                ModEntry.GetInstance().Monitor.Log($"Error in MonsterDrop_Postfix: {ex}", LogLevel.Error);
            }
        }
    }
}