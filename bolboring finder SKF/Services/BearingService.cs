using bolboring_finder_SKF.Models;
using System.IO;
using System.Text.Json;
using System.Windows;

public class BearingService
{
    private List<Bearing> _bearings;

    public BearingService()
    {
        LoadData();
    }

    private void LoadData()
    {
        string path = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "bearings.json");

        string json = File.ReadAllText(path);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        _bearings = JsonSerializer.Deserialize<List<Bearing>>(json, options)
                            ?? new List<Bearing>();
    }
    public List<Bearing> Search(
        double? innerDiameter,
        double? outerDiameter,
        double? Width

        )
    {
        var query = _bearings.AsQueryable();

        if (innerDiameter.HasValue)
        {
            query = query.Where(x =>
                x.InnerDiameter == innerDiameter.Value);
        }
        if (outerDiameter.HasValue)
        {
            query = query.Where(x =>
                x.InnerDiameter == outerDiameter.Value);
        }
        if (Width.HasValue)
        {
            query = query.Where(x =>
                x.InnerDiameter == Width.Value);
        }
        return query.ToList();
    }
}