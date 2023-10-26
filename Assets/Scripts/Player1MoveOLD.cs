using UnityEngine;
using Unity.Netcode;

public class Player1MoveOLD : PlayerBaseOLD
{
    protected override string HorizontalInputAxis => "Horizontal_p1";
    protected override string JumpInputButton => "Jump_p1";

}