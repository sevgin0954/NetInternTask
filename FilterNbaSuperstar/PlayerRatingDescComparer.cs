using FilterNbaSuperstar.Models;
using System.Collections.Generic;

namespace FilterNbaSuperstar
{
    public class PlayerRatingDescComparer : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            return y.Rating.CompareTo(x.Rating);
        }
    }
}
