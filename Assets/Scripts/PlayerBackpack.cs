using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpack
{
    public const int id = 0;
    public int money;

    public PlayerBackpack()
    {
        money = 1000;
    }
}

public class StoreBackpack
{
    public const int id = 1;

    public int money;
    
    public StoreBackpack()
    {
        money = 1000;
    }
}
