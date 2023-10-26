using UnityEngine;
using Unity.Netcode;

public class Player1Move : PlayerBase
{
    protected override string HorizontalInputAxis => "Horizontal_p1";
    protected override string JumpInputButton => "Jump_p1";

}