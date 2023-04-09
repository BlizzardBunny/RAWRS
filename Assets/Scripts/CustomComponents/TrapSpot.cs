using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpot : MonoBehaviour
{
    public int direction;
    public CatTrappingArea parent;

    public void ConfirmTrap()
    {
        parent.ConfirmTrap(direction);
    }
}
