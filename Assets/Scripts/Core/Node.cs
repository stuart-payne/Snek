using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Snek.Core
{
    public class Node : INode
    {
        private Board m_Board;
        public GridPiece GridPiece;

        public Node(Board board, GridPiece gridPiece)
        {
            m_Board = board;
            GridPiece = gridPiece;
            Cost = 1;
        }

        public INode[] GetNeighbours()
        {
            var neighbours = m_Board.GetGridPieceNeighbours(GridPiece);
            var nodeArray = new Node[neighbours.Count];
            for (int i = 0; i < neighbours.Count; i++)
            {
                nodeArray[i] = new Node(m_Board, neighbours[i]);
            }

            return nodeArray;
        }
        
        public int Cost { get; set; }
    }
}
