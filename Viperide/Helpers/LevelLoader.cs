using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class LevelLoader
{
    public static void Load(Tilemap tilemap, string layerName, int level = 1)
    {
        string basPath = Directory.GetCurrentDirectory();

        string filePath = Path.Combine(basPath, "Ressources", "Levels", $"level_{level}.txt");
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int columns = lines[0].Length;

            for (int row = 0; row < rows; row++)
            {
                for(int column=0;  column < columns; column++)
                {
                    tilemap.SetTile(new(column, row), layerName, lines[row][column] == '1');
                }
            }
        }
        else
            Console.WriteLine($"File not found : {filePath}" );
    }
}
