using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : Enemy
{
    protected override void PlayerImpact(Player player)
    {
        if (player.IsInvincible) return;

        player.GetComponent<Health>().OnDamage(999);
    }
}
