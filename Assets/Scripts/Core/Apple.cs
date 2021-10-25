using System.Collections;
using System.Collections.Generic;
using Snek.Core;
using UnityEngine;

namespace Snek.Core
{
    public class Apple : MonoBehaviour, IGridItem
    {
        public GridItem GridItemType => GridItem.Apple;
    }
}
