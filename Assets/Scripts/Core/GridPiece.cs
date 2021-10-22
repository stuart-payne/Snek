using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public class GridPiece : MonoBehaviour
    {
        private IGridItem m_GridItem;
        
        public GridItem Contains()
        {
            return m_GridItem?.GridItemType ?? GridItem.None;
        }

        public void AddGridItem(IGridItem gridItem) => m_GridItem = gridItem;
        public void RemoveItem() => m_GridItem = null;
    }
}
