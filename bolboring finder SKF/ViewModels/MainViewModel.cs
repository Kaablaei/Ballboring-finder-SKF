using bolboring_finder_SKF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

public partial class MainViewModel : ObservableObject
{
    private readonly BearingService _service;

    [ObservableProperty]
    private double? innerDiameter;

    [ObservableProperty]
    private double? outerDiameter;

    [ObservableProperty]
    private bool? waterproof;

    public ObservableCollection<Bearing> Results { get; set; }

    public MainViewModel()
    {
        _service = new BearingService();

        Results = new ObservableCollection<Bearing>();
    }

    [RelayCommand]
    private void Search()
    {
        Results.Clear();

        var bearings = _service.Search(
            InnerDiameter,
            OuterDiameter,
            Waterproof);

        foreach (var item in bearings)
        {
            Results.Add(item);
        }
    }
}