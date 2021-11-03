using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public class GridPiece : MonoBehaviour, INode
    {
        private IGridItem m_GridItem;

        public Board BoardRef;

        public int Cost => 1;

        public INode[] GetNeighbours() => BoardRef.GetGridPieceNeighbours(this).ToArray();

        public GridItem Contains()
        {
            return m_GridItem?.GridItemType ?? GridItem.None;
        }

        public void AddGridItem(IGridItem gridItem) => m_GridItem = gridItem;
        public void RemoveItem() => m_GridItem = null;
        public Vector2Int GridPosition { get; set; }
    }
}
