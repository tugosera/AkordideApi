# Akordide API - Music Chord Management System

A .NET 8 Web API for managing musical chords (kolmkõlad) and songs (lood) with support for MIDI note numbers and letter name conversions.

## Features

- **Kolmkõla (Triad) Management**: Create and manage major triads with automatic calculation of chord notes
- **MIDI ↔ Letter Conversion**: Bidirectional conversion between MIDI note numbers and letter names
- **Chord Subclasses**: Pre-configured chord classes for C, F, and G
- **Song Management**: Create songs with chord progressions across multiple measures
- **Flexible Output**: Display chord notes as either MIDI numbers or letter names
- **RESTful API**: Full CRUD operations for chords and songs

## Project Structure

```
AkordideApi/
├── Models/
│   ├── Kolmkola.cs          # Base triad class
│   ├── CKolmkola.cs         # C chord (MIDI 60)
│   ├── FKolmkola.cs         # F chord (MIDI 65)
│   ├── GKolmkola.cs         # G chord (MIDI 67)
│   └── Lugu.cs              # Song class with measures
├── Controllers/
│   ├── KolmkoladController.cs
│   └── LoodController.cs
├── Data/
│   └── AkordContext.cs      # Entity Framework context
├── Tests/
│   └── KolmkolaTests.cs     # Unit tests
└── Program.cs               # Application entry point
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server LocalDB (or configure your own connection string)

### Installation

1. Clone the repository
2. Navigate to the project directory:
   ```bash
   cd AkordideApi
   ```

3. Restore dependencies:
   ```bash
   dotnet restore
   ```

4. Apply database migrations:
   ```bash
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:7162` (or the port specified in your launch settings).

## API Endpoints

### Kolmkõlad (Chords)

- `GET /api/kolmkolad` - Get all chords
- `GET /api/kolmkolad/{id}` - Get a specific chord
- `POST /api/kolmkolad` - Create a new chord
  ```json
  {
    "Tahis": "C"  // or "Pohitoon": 60
  }
  ```
- `PUT /api/kolmkolad/{id}` - Update a chord
- `DELETE /api/kolmkolad/{id}` - Delete a chord
- `GET /api/kolmkolad/{id}/noodid?formaat=arv` - Get chord notes as numbers
- `GET /api/kolmkolad/{id}/noodid?formaat=nimed` - Get chord notes as letter names

### Lood (Songs)

- `GET /api/lood` - Get all songs
- `GET /api/lood/{id}` - Get a specific song
- `POST /api/lood` - Create a new song
  ```json
  {
    "Nimetus": "My Song",
    "Taktid": []
  }
  ```
- `PUT /api/lood/{id}` - Update a song
- `DELETE /api/lood/{id}` - Delete a song
- `GET /api/lood/{id}/noodid?formaat=arv` - Get all song notes as numbers
- `GET /api/lood/{id}/noodid?formaat=nimed` - Get all song notes as letter names

## Usage Examples

### Creating a C Major Chord

**Request:**
```http
POST /api/kolmkolad
Content-Type: application/json

{
  "Tahis": "C"
}
```

**Response:**
```json
{
  "id": 1,
  "pohitoon": 60,
  "tahis": "C"
}
```

### Getting Chord Notes

**Request:**
```http
GET /api/kolmkolad/1/noodid?formaat=nimed
```

**Response:**
```json
["C", "E", "G"]
```

### Creating a Song with Chord Progression

1. Create chords (C, F, G)
2. Create a song
3. Add measures with chord references

## Testing

Run the unit tests:

```bash
cd Tests
dotnet test
```

Tests cover:
- Chord creation and note calculation
- MIDI ↔ letter name conversion
- Song management with multiple measures
- All chord subclasses (C, F, G)

## Note Naming Convention

The system uses the following note names:
- C, C#, D, Eb, E, F, F#, G, G#, A, B, H

Where:
- MIDI 60 = C (middle C)
- MIDI 71 = H (B in German notation)
- Each semitone = +1 MIDI number

## Major Triad Formula

For any root note, the major triad consists of:
- Root note (0 semitones)
- Major third (+4 semitones)
- Perfect fifth (+7 semitones)

Example: C major (60) → [60, 64, 67] → [C, E, G]

## Database

The application uses Entity Framework Core with SQL Server. The database schema includes:
- `Kolmkolad` table (with TPH inheritance for subclasses)
- `Lood` table
- `Taktid` table (measures linking songs to chords)

## Configuration

Database connection string can be configured in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AkordidDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

## CORS

The API is configured to allow all origins for development. Update the CORS policy in `Program.cs` for production use.

## License

This project is for educational purposes.
