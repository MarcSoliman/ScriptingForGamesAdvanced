using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    [SerializeField] private float _slowSpeed;
    protected override void PlayerImpact(Player player)
    {
        TankController tankController = player.GetComponent<TankController>();
        if (tankController != null)
        {
            tankController.MaxSpeed -= _slowSpeed;
            tankController.TurnSpeed -= _slowSpeed;
        }
    }
}
