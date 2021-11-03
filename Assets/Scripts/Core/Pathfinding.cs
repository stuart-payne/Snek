using System;
using System.Collections.Generic;
using Priority_Queue;

namespace Snek.Core
{
    public static class Pathfinding
    {
        public static T[] BreadthFillSearch<T>(T start, T goal) where T: INode
        {
            var frontier = new Queue<T>();
            frontier.Enqueue(start);
            var cameFrom = new Dictionary<T, T>();

            T current;
            bool found = false;
            while (frontier.Count != 0)
            {
                current = frontier.Dequeue();
                if (Equals(current, goal))
                {
                    found = true;
                    break;   
                }
                foreach (T next in current.GetNeighbours())
                {
                    if(cameFrom.ContainsKey(next))
                        continue;
                    frontier.Enqueue(next);
                    cameFrom.Add(next, current);
                }
            }
        
            var path = new List<T>();
            if (!found)
                return Array.Empty<T>();
            current = goal;
            path.Add(current);
            while (!Equals(current, start))
            {
                var nextNode = cameFrom[current];
                path.Add(nextNode);
                current = nextNode;
            }

            return path.ToArray();
        }

        public static T[] AStar<T>(T start, T goal, Func<T, T, float> heuristic) where T : INode
        {
            var frontier = new SimplePriorityQueue<T, float>();
            var cameFrom = new Dictionary<T, T>();
            var costSoFar = new Dictionary<T, float>();

            cameFrom[start] = default(T);
            costSoFar[start] = 0;
            frontier.Enqueue(start, 0);

            bool goalFound = false;

            T current;
        
            while (frontier.Count != 0)
            {
                current = frontier.Dequeue();
                if (Equals(current, goal))
                {
                    goalFound = true;
                    break;
                }

                foreach (T next in current.GetNeighbours())
                {
                    var newCost = costSoFar[current] + next.Cost;
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        var priority = newCost + heuristic(next, goal);
                        frontier.Enqueue(next, priority);
                        cameFrom[next] = current;
                    }
                }
            }

            if (!goalFound)
                return Array.Empty<T>();

            current = goal;
            var path = new List<T>();
            path.Add(current);
            while (cameFrom[current] != null)
            {
                var next = cameFrom[current];
                path.Add(next);
                current = next;
            }

            return path.ToArray();
        }
    }
}
