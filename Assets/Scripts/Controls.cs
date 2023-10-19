using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    private static PlayerAction controls;
    public static void Init(Player player)
    {
        controls = new PlayerAction();

        controls.Game.Move.performed += ctx =>
        {
            player.SetMovementDirection(ctx.ReadValue<Vector2>());
        };
        PlayMode();
    }
    public static void PlayMode()
    {
        controls.Enable();
    }
}
