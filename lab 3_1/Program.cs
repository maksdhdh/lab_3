using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class Vector
{
    public List<double> Coordinates { get; set; }

    public Vector(List<double> coordinates)
    {
        this.Coordinates = coordinates;
    }

    public void IncreaseCoordinates()
    {
        this.Coordinates.Add(0);
    }

    public void DecreaseCoordinates()
    {
        if (this.Coordinates.Count > 0)
        {
            this.Coordinates.RemoveAt(this.Coordinates.Count - 1);
        }
    }

    public static Vector Add(Vector v1, Vector v2)
    {
        var result = v1.Coordinates.Zip(v2.Coordinates, (c1, c2) => c1 + c2).ToList();
        return new Vector(result);
    }

    public static Vector Subtract(Vector v1, Vector v2)
    {
        var result = v1.Coordinates.Zip(v2.Coordinates, (c1, c2) => c1 - c2).ToList();
        return new Vector(result);
    }

    public static Vector Multiply(Vector v, double scalar)
    {
        var result = v.Coordinates.Select(c => c * scalar).ToList();
        return new Vector(result);
    }

    public double Length()
    {
        return Math.Sqrt(this.Coordinates.Sum(c => c * c));
    }

    public void SaveToJson(string filePath)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(this, options);
        File.WriteAllText(filePath, json);
    }

    public static Vector LoadFromJson(string filePath)
    {
        var json = File.ReadAllText(filePath);
        var vector = JsonSerializer.Deserialize<Vector>(json);
        return vector;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Vector v1 = new Vector(new List<double> { 3, 4, 6 });
        Vector v2 = new Vector(new List<double> { 7, 5, 6 });

        v1.IncreaseCoordinates();
        v1.DecreaseCoordinates();

        Vector sum = Vector.Add(v1, v2);
        Vector diff = Vector.Subtract(v1, v2);
        Vector prod = Vector.Multiply(v1, 2);

        double length = v1.Length();

        var results = new Dictionary<string, object>
        {
            {"v1", v1.Coordinates},
            {"v2", v2.Coordinates},
            {"sum", sum.Coordinates},
            {"difference", diff.Coordinates},
            {"product", prod.Coordinates},
            {"length", length}
        };

        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(results, options);
        File.WriteAllText("results.json", json);

        
    }
}
