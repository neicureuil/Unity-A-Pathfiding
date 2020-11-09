using UnityEngine;
using System.Collections;
using System;

namespace NPathfinding {

    public class Node : IHeapItem<Node> {

        //Node params
        public bool walkable;

        //Pathfinding Stuff
        public int gCost;
        public int hCost;
        public int gridX;
        public int gridY;
        public Node parent;
        private int heapIndex;

        public Node(bool _walkable) {
            this.walkable = _walkable;
        }

        public void SetPos(int x, int y) {
            this.gridX = x;
            this.gridY = y;
        }

        public int fCost {
            get {
                return gCost + hCost;
            }
        }

        public int HeapIndex {
            get {
                return heapIndex;
            }
            set {
                heapIndex = value;
            }
        }

        public int CompareTo(Node nodeToCompare) {
            int compare = fCost.CompareTo(nodeToCompare.fCost);
            if (compare == 0) {
                compare = hCost.CompareTo(nodeToCompare.hCost);
            }
            return -compare;
        }

    }

}
