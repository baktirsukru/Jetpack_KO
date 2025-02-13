using UnityEngine;

public class Coin : Pickup
{
    int point;
    protected override void OnPickup()
    {
        point = point + 100;
        Debug.Log(point);
    }
}
