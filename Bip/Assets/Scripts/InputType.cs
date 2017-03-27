using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputType : ScriptableObject
{
    public EntityScript.Type type = EntityScript.Type.None;
    public InputMngr.Side side;

    public InputType(EntityScript.Type type, InputMngr.Side side)
    {
        this.type = type;
        this.side = side;
    }
}