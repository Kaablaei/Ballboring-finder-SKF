# SKF Bearing Finder

A small WPF (.NET, MVVM) desktop application built as a course project for "Machine Elements Design". It lets a user search a local catalog of SKF deep groove ball bearings by dimensions and shield/seal options.

## Features

- Search bearings by **inner diameter**, **outer diameter**, and **width**.
- Filter by **shield type**:
  - `Z` — single metal shield
  - `ZZ` — double metal shield
  - None — open bearing
- Results are shown with the matching SKF serial number (shield suffix appended automatically).
- Bearing catalog is loaded from a local `bearings.json` file — no external API or database required.

## Tech Stack

- **.NET / WPF** — UI framework
- **CommunityToolkit.Mvvm** — `ObservableObject`, `[ObservableProperty]`, `[RelayCommand]` source generators for MVVM
- **System.Text.Json** — catalog deserialization

## Project Structure

```
bolboring_finder_SKF/
├── Models/
│   └── Bearing.cs            # Bearing data model (Serial, InnerDiameter, OuterDiameter, Width, Waterproof)
├── Services/
│   └── BearingService.cs     # Loads catalog from JSON and performs search/filtering
├── ViewModels/
│   └── MainViewModel.cs      # Search command, shield-type logic, bound properties
├── Data/
│   └── bearings.json         # Bearing catalog (96 SKF deep groove ball bearing records)
└── README.md
```

## Bearing Catalog

`Data/bearings.json` contains standard SKF deep groove ball bearings from the **60xx (extra light)**, **62xx (light)**, and **63xx (medium)** series, with real dimensions per the ISO 15 / DIN 625 standard. Each entry includes:

| Field | Description |
|---|---|
| `serial` | SKF part number, including shield/seal suffix (e.g. `6204-2RS1`, `6206-ZZ`) |
| `innerDiameter` | Bore diameter (mm) |
| `outerDiameter` | Outer diameter (mm) |
| `width` | Bearing width (mm) |
| `waterproof` | `true` for rubber-sealed variants (`RS1` / `2RS1`), `false` for open or shielded variants |
| `sealType` | Human-readable seal/shield description |

## How Search Works

1. The user enters any combination of inner diameter, outer diameter, and width, and optionally checks "metal shield" and/or "double side".
2. `BearingService.Search()` filters the in-memory catalog against each supplied dimension.
3. `MainViewModel` derives the requested `ShieldType` (`None`, `Z`, or `ZZ`) from the checkboxes and appends the matching suffix to each result's serial number for display.

## Known Fix Applied

The original `BearingService.Search()` compared **all three** filters (`innerDiameter`, `outerDiameter`, `width`) against `x.InnerDiameter`, so filtering by outer diameter or width had no effect. This has been corrected so each parameter now compares against its own property (`x.OuterDiameter`, `x.Width`).

## Running the Project

1. Open the solution in Visual Studio (or `dotnet build` from the CLI).
2. Make sure `Data/bearings.json` is set to **"Copy to Output Directory: Copy if newer"** so it's available next to the built executable.
3. Run the app, enter search criteria, and click **Search**.

## Possible Improvements

- Add unit tests for `BearingService.Search()`.
- Support a tolerance/range search (e.g. ±1 mm) instead of exact-match doubles.
- Validate and surface a friendly error if `bearings.json` is missing or malformed, instead of letting `File.ReadAllText` throw unhandled.
- Expand the catalog with load ratings, limiting speed, and weight for more realistic engineering selection.
