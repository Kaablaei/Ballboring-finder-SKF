using bolboring_finder_SKF.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.VisualBasic.FileIO;
using System.Collections.ObjectModel;

public partial class MainViewModel : ObservableObject
{
    private readonly BearingService _service;

    [ObservableProperty]
    private double? innerDiameter;

    [ObservableProperty]
    private double? outerDiameter;

    [ObservableProperty]
    private double? width;

    [ObservableProperty]
    private bool metalShield;

    [ObservableProperty]
    private bool doubleSide;
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
            width);

        var shieldType = GetShieldType();

        foreach (var item in bearings)
        {
            switch (shieldType)
            {
                case ShieldType.Z:
                    item.Serial += "Z";
                    break;

                case ShieldType.ZZ:
                    item.Serial += "ZZ";
                    break;

                case ShieldType.None:
                default:
                    break;
            }

            Results.Add(item);
     
        }

    }
    private ShieldType GetShieldType()
    {
        if (metalShield && doubleSide)
            return ShieldType.ZZ;

        if (metalShield)
            return ShieldType.Z;

        return ShieldType.None;
    }
}