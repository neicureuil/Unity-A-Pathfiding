using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPathfinding {

    public class Pathfinding : MonoBehaviour {

        [Header("Pathfinding Config")]
        public PathfindingConfig findingConfig;
        private Grid grid;

        void Awake() {
            grid = GetComponent<Grid>();
        }

        public List<Node> GetPath(Vector2 startPos, Vector2 targetPos) {
            return FindPath(startPos, targetPos);
        }

        List<Node> FindPath(Vector2 startPos, Vector2 targetPos) {

            Node startNode = grid.GetNode(startPos);
            Node targetNode = grid.GetNode(targetPos);

            Heap<Node> openSet = new Heap<Node>(grid.MaxSize);
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == targetNode) {
                    return RetracePath(startNode, targetNode);
                }

                foreach (Node neighbour in grid.GetNeighbours(currentNode, findingConfig)) {
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                        continue;
                    }

                    int newMovCostToNeigh = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovCostToNeigh < neighbour.gCost || !openSet.Contains(neighbour)) {
                        neighbour.gCost = newMovCostToNeigh;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;
                        if (!openSet.Contains(neighbour)) {
                            openSet.Add(neighbour);
                        }
                    }
                }

            }

            return null;
        }

        List<Node> RetracePath(Node startNode, Node endNode) {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;
            while (currentNode != startNode) {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }
            path.Reverse();
            return path;
        }

        private int GetDistance(Node nodeA, Node nodeB) {

            int disX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int disY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (disX > disY) {
                return 14 * disY + 10 * (disX - disY);
            }
            return 14 * disX + 10 * (disY - disX);

        }

    }

}
