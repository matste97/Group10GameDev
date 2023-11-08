using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerController", menuName = "InputController/PlayerController")]

public class PlayerController : InputController
{
    public override bool RetrieveJumpIntput()
    {
        return Input.GetButtonDown("Jump");
    }
    public override float RetrieveMoveIntput()
    {
        return Input.GetAxisRaw("Horizontal");
    }


}
