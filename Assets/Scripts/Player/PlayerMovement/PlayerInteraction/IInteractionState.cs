using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractionState
{
    void HandleInput(PlayerInteraction playerInteraction);
}
