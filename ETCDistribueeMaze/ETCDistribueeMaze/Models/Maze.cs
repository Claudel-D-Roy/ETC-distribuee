using ETCDistribueeMaze.ViewModels;
using System;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;


//Algorithme de Prim
namespace ETCDistribueeMaze.Models
{
    public class Maze
    {
        private int width;
        private int height;
        private bool[,] grid;
        private int Doorx;
        private int Doory;
        private int Exitx;
        private int Exity;
        private bool isFirstPlacement = true;
        public bool EndGame { get; set; }   
        public Dictionary<string, Position> PlayerPositions { get; } = new Dictionary<string, Position>();

        public Maze(int rows, int columns)
        {
            width = rows;
            height = columns;

            grid = new bool[width, height];
            EndGame = false;
            PlayerPositions = new Dictionary<string, Position>();
            DisplayMaze();
        }

        public int widthMaze { get { return width; } set { width = value; }
        }
        public int heightMaze { get { return height; } set { height = value; } }
        public int doorx { get { return Doorx; } set { Doorx = value; } }
        public int doory { get { return Doory; } set { Doory = value; } }
        public int exitx { get { return Exitx; } set { Exitx = value; } }
        public int exity { get { return Exity; } set { Exity = value; } }
        public bool endgame { get { return EndGame; } set { EndGame = value; } }

        public void SetMaze(bool[,] gridEntrant, int doorx, int doory, int exitx, int exity, bool endgame)
        {
           
            if (grid.GetLength(0) == widthMaze && grid.GetLength(1) == heightMaze)
            {
                for (int i = 0; i < widthMaze; i++)
                {
                    for (int j = 0; j < heightMaze; j++)
                    {
                        grid[i, j] = gridEntrant[i, j];
                    }
                }
            }
            Doorx = doorx;
            Doory = doory;
            Exitx = exitx;
            Exity = exity;
            EndGame = endgame;
         


        }


        public bool[,] GenerateMaze()
        {
           
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    grid[i, j] = true;
                }
            }

            Random random = new Random();
            int startX = random.Next(0, width);
            int startY = random.Next(0, height);

      
            VisitCell(startX, startY);
            PlaceEntranceAndExit();
            return  grid;
        }

        public bool[,] GetMaze()
        {
            return grid;
        }

        private void PlaceEntranceAndExit()
        {
            Random random = new Random();
            int entranceX = 0;
            int entranceY = random.Next(1, height - 1);
            int exitX = width - 1;
            int exitY = random.Next(0, height - 1);


            if (IsTrueBlockNearby(entranceX, entranceY))
            {
                if (grid[entranceX + 1 , entranceY+1] == false)
                {
                    entranceY++;
                }else if (grid[entranceX + 2, entranceY+1] == false)
                {
                    grid[entranceX + 1, entranceY+1] = false;
                    entranceY++;
                }
              
            }

            if (IsTrueBlockNearby(exitX, exitY))
            {
                if (grid[exitX - 1, exitY+1] == false)
                {
                    exitY++;
                }
                else if (grid[exitX - 2, exitY+1] == false)
                {
                    grid[exitX - 1, exitY+1] = false;
                    exitY++;
                }
                
            }

            
            grid[entranceX, entranceY] = false;
            grid[exitX, exitY] = false; 
            Doorx = entranceX;
            Doory = entranceY;
            Exitx = exitX;
            Exity = exitY;
        }
        
        private bool IsTrueBlockNearby(int x, int y)
        {

            return IsValidCell(x - 1, y) && grid[x - 1, y] || // Gauche
                   IsValidCell(x + 1, y) && grid[x + 1, y] || // Droite
                   IsValidCell(x, y - 1) && grid[x, y - 1] || // Haut
                   IsValidCell(x, y + 1) && grid[x, y + 1];    // Bas
        }
     

        private void VisitCell(int x, int y)
        {
           
            grid[x, y] = false;

            int[] directions = { 0, 1, 2, 3 };
            Shuffle(directions);

            foreach (var direction in directions)
            {
                int nextX = x + 2 * DX[direction];
                int nextY = y + 2 * DY[direction];

                if (IsValidCell(nextX, nextY))
                {
                    if (IsSideCell(nextX, nextY))
                        grid[nextX, nextY] = true;
                    else if (grid[nextX, nextY])
                    {      
                        grid[nextX, nextY] = false; 
                        grid[x + DX[direction], y + DY[direction]] = false;
                        VisitCell(nextX, nextY);
                    }
                }
            }
        }
     
       
        private void DisplayMaze()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Console.Write(grid[i, j] ? "X" : " ");
                }
                Console.WriteLine();
            }
        }

        private bool IsSideCell(int x, int y)
        {

            return x == 0 || x == width - 1 || y == 0 || y == height - 1;
        }

        private bool IsValidCell(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        private void Shuffle(int[] array)
        {
            Random random = new Random();
            int n = array.Length;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }

        private int[] DX = { 0, 1, 0, -1 };
        private int[] DY = { -1, 0, 1, 0 };




        public void SetPlayerPosition(string playerId, int row, int col)
        {
            if (PlayerPositions.ContainsKey(playerId))
            {
                var previousPosition = PlayerPositions[playerId];
                grid[previousPosition.PosX, previousPosition.PosY] = false;
            }
            else
            {
                isFirstPlacement = true;
            }

            if (isFirstPlacement)
            {
                PlayerPositions[playerId] = new Position(playerId, Doorx, Doory);
                grid[Doorx, Doory] = true;

                isFirstPlacement = false;
            }
            else if (!grid[row, col] && IsValidPosition(row, col)) // Check if the position is valid
            {
                PlayerPositions[playerId] = new Position(playerId, row, col);
                grid[row, col] = true;
                if (IsExitPosition(row, col))
                {
                   EndGame = true;
                }
            }
            else
            {
                var newPosition = FindNextAvailablePosition(row, col);

                if (newPosition != null)
                {
                    PlayerPositions[playerId] = new Position(playerId, newPosition.PosX, newPosition.PosY);
                    grid[newPosition.PosX, newPosition.PosY] = true;

                    if (IsExitPosition(newPosition.PosX, newPosition.PosY))
                    {
                       EndGame = true;
                    }
                }
                else
                {
                    Console.WriteLine("Aucune position disponible n'a été trouvée.");
                }
            }
        }

        private bool IsExitPosition(int row, int col)
        {

            return row == Exitx && col == Exity;
        }

        private Position FindNextAvailablePosition(int startRow, int startCol)
        {
            for (int i = 1; i <= 3; i++)
            {
                for (int j = -i; j <= i; j++)
                {
                    if (IsValidPosition(startRow - i, startCol + j) && !grid[startRow - i, startCol + j])
                    {
                        return new Position("", startRow - i, startCol + j);
                    }

                    if (IsValidPosition(startRow + i, startCol + j) && !grid[startRow + i, startCol + j])
                    {
                        return new Position("", startRow + i, startCol + j);
                    }

                    if (IsValidPosition(startRow + j, startCol - i) && !grid[startRow + j, startCol - i])
                    {
                        return new Position("", startRow + j, startCol - i);
                    }

                    if (IsValidPosition(startRow + j, startCol + i) && !grid[startRow + j, startCol + i])
                    {
                        return new Position("", startRow + j, startCol + i);
                    }
                }
            }

            return null;
        }

        private bool IsValidPosition(int row, int col)
        {
            return row >= 0 && row < width && col >= 0 && col < height;
        }

        public void MovePlayer(string playerId, int newRow, int newCol)
        {
            if (IsValidMove(playerId, newRow, newCol))
            {
                Position currentPosition = PlayerPositions[playerId];
                grid[currentPosition.PosX, currentPosition.PosY] = false;
                SetPlayerPosition(playerId, newRow, newCol);
            }
        }

        private bool IsValidMove(string playerId, int row, int col)
        {
            if (row < 0 || row >= width || col < 0 || col >= height)
            {
                return false;
            }

            return !grid[row, col] && !PlayerPositions.Values.Any(p => p.PosX == row && p.PosY == col && p.PlayerID != playerId);
        }
    }
}