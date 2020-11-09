using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPathfinding {

    public class Grid : MonoBehaviour {

        public Vector2Int gridSize;
        private Node[,] grid;
        private int gridSizeX, gridSizeY;

        void Awake() {
            gridSizeX = gridSize.x;
            gridSizeY = gridSize.y;
            CreateGrid();
        }

        void CreateGrid() {
            grid = new Node[gridSizeX, gridSizeY];

            for (int x = 0; x < gridSizeX; x++) {
                for (int y = 0; y < gridSizeY; y++) {
                    grid[x, y] = new Node(true);
                    grid[x, y].SetPos(x, y);
                }
            }
        }

        public List<Node> GetNeighbours(Node node, PathfindingConfig config) {
            List<Node> neighbours = new List<Node>();

            for (int x = -1; x <= 1; x++) {
                for (int y = -1; y <= 1; y++) {

                    if (config.AllowDiagonal) {
                        if (x == 0 && y == 0) continue;
                    } else {
                        if (x == 0 && y == 0 || x == -1 && y == -1 || x == 1 && y == -1 || x == -1 && y == 1 || x == 1 && y == 1) continue;
                    }

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (isInGrid(checkX, checkY)) {
                        neighbours.Add(grid[checkX, checkY]);
                    }

                }
            }

            return neighbours;
        }

        public void SetWalkable(int x, int y, bool walk) {
            if (isInGrid(x, y)) {
                grid[x, y].walkable = walk;
            }
        }

        public void SetWalkable(Vector2 pos, bool walk) {
            SetWalkable((int)pos.x, (int)pos.y, walk);
        }

        public bool isInGrid(int x, int y) {
            if (x >= 0 && x < gridSizeX && y >= 0 && y < gridSizeY) {
                return true;
            }
            return false;
        }

        public bool isInGrid(Vector2 pos) {
            return isInGrid((int)pos.x, (int)pos.y);
        }

        public void SetNode(int x, int y, Node node) {
            if (isInGrid(x, y)) {
                grid[x, y] = node;
            }
        }

        public void SetNode(Vector2 pos, Node node) {
            SetNode((int)pos.x, (int)pos.y, node);
        }

        public Node GetNode(int x, int y) {
            if (isInGrid(x, y)) {
                return grid[x, y];
            }
            return null;
        }

        public Node GetNode(Vector2 pos) {
            return GetNode((int)pos.x, (int)pos.y);
        }

        public int MaxSize {
            get {
                return gridSizeX * gridSizeY;
            }
        }
    }

}
