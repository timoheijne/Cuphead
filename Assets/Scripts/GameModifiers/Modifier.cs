using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Modifier
{
    public abstract void StartMod(Randomiser r);
    public abstract void UpdateMod(Randomiser r);
    public abstract void DestroyMod(Randomiser r);
}
