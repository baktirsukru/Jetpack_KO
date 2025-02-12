using UnityEngine;

public class PowerUp : Pickup
{
    protected override void OnPickup()
    {
        Debug.Log("PowerUp Collected");
    }
}

