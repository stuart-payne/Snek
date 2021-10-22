using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public class SnakePiece : MonoBehaviour, IGridItem
    {
        public Vector2Int GridPosition;
        public GridItem GridItemType => GridItem.Snake;
    }
}
