using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public class GameInput : MonoBehaviour, IGameInput
    {
        public Direction DirectionToMove { get; private set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                DirectionToMove = Direction.Up;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                DirectionToMove = Direction.Right;
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                DirectionToMove = Direction.Down;
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                DirectionToMove = Direction.Left;
        }
    }
}
