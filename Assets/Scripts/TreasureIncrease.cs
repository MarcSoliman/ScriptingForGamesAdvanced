using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureIncrease : CollectibleBase
{
    [SerializeField] private int _treasureValue;
    

    protected override void Collect(Player player)
    {
       Inventory inventory = player.GetComponent<Inventory>();
       inventory.Treasure += _treasureValue;
    }
}
