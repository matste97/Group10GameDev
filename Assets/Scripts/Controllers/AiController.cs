using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AiController", menuName = "InputController/AiController")]

public class AiController : InputController
{
    public override bool RetrieveJumpIntput()
    {
        return true;
    }

    public override float RetrieveMoveIntput()
    {
        return 1f;
    }
}
