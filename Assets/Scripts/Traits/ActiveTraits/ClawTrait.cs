using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawTrait : Trait, IActiveTrait
{
    public int StaminaRequired
    {
        get { return 1; }
    }
    public override string Name
    {
        get { return "Claw Trait"; }
    }
    public override int Cost
    {
        get { return 1; }
    }
    public override Environment Environment
    {
        get { return Environment.Desert; }
    }

    public void DoActive(PlayerController player)
    {
        var rotation = 0f;
        /*if (player.XAxis > 0)
        {
            rotation = 90;
        }
        if (player.YAxis > 0)
        {
            rotation = 180;
        }
        if (player.XAxis < 0)
        {
            rotation = -90;
        }
        if (player.YAxis < 0)
        {
            rotation = -180;
        }*/
        
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDir = mousePos - player.transform.position;
        mouseDir.z = 0.0f;
        mouseDir = mouseDir.normalized;
        rotation = 180f + (float) Math.Atan2(mouseDir.x, mouseDir.y) * (-180f / (float) Math.PI);
        
        var claws = player.gameObject.transform.Find("Claws").gameObject;
        claws.SetActive(true);
        claws.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
