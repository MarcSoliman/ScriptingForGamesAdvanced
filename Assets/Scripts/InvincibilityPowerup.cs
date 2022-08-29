using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvincibilityPowerup : PowerUpBase
{
    private Material _bodyMaterial;
    private Color _originalColor;
    
    
    

    protected override void PowerUp(Player player)
    {
        player.IsInvincible = true;
        //get body game object
        GameObject body = player.transform.Find("Art").transform.Find("Body").GameObject();
        //get body material
        _bodyMaterial = body.GetComponent<Renderer>().material;
        //save original color
        _originalColor = _bodyMaterial.color;
        
        _bodyMaterial.color = Color.cyan;
    }

    protected override void PowerDown(Player player)
    {
        player.IsInvincible = false;
        _bodyMaterial.color = _originalColor;
    }
}
