using Xunit;
using AkordideApi.Models;
using System.Linq;

namespace AkordideApi.Tests
{
    public class KolmkolaTests
    {
        [Fact]
        public void CKolmkola_olekutest()
        {
            var c = new CKolmkola();
            var noodid = c.GetNoodid();
            Assert.Equal(new int[] { 60, 64, 67 }, noodid);
            var nimed = c.GetNimed();
            Assert.Equal(new string[] { "C", "E", "G" }, nimed);
        }

        [Fact]
        public void FKolmkola_test()
        {
            var f = new FKolmkola();
            var noodid = f.GetNoodid();
            Assert.Equal(new int[] { 65, 69, 72 }, noodid);
            var nimed = f.GetNimed();
            Assert.Equal(new string[] { "F", "A", "C" }, nimed);
            Assert.Equal(65, f.Pohitoon);
            Assert.Equal("F", f.Tahis);
        }

        [Fact]
        public void GKolmkola_test()
        {
            var g = new GKolmkola();
            var noodid = g.GetNoodid();
            Assert.Equal(new int[] { 67, 71, 74 }, noodid);
            var nimed = g.GetNimed();
            Assert.Equal(new string[] { "G", "H", "D" }, nimed);
            Assert.Equal(67, g.Pohitoon);
            Assert.Equal("G", g.Tahis);
        }

        [Fact]
        public void NameToMidi_ja_MidiToName()
        {
            Assert.Equal(60, Kolmkola.NameToMidi("C"));
            Assert.Equal("C", Kolmkola.MidiToName(60));
            Assert.Equal("G", Kolmkola.MidiToName(67));
            Assert.Equal("F", Kolmkola.MidiToName(65));
            Assert.Equal(65, Kolmkola.NameToMidi("F"));
        }

        [Fact]
        public void ConstructByName()
        {
            var g = new Kolmkola("G");
            Assert.Equal(67, g.Pohitoon);
            Assert.Equal(new int[] { 67, 71, 74 }, g.GetNoodid());
            
            var f = new Kolmkola("F");
            Assert.Equal(65, f.Pohitoon);
            Assert.Equal(new int[] { 65, 69, 72 }, f.GetNoodid());
        }

        [Fact]
        public void Lugu_AddMultipleMeasures()
        {
            var lugu = new Lugu { Nimetus = "Test Song" };
            
            // Create chords
            var cChord = new CKolmkola();
            var fChord = new FKolmkola();
            var gChord = new GKolmkola();
            
            // Add measures (taktid)
            lugu.Taktid.Add(new Takt { Kolmkola = cChord });
            lugu.Taktid.Add(new Takt { Kolmkola = fChord });
            lugu.Taktid.Add(new Takt { Kolmkola = gChord });
            lugu.Taktid.Add(new Takt { Kolmkola = cChord });
            
            // Test GetNoodidArvuliselt
            var arvuliselt = lugu.GetNoodidArvuliselt().ToList();
            Assert.Equal(4, arvuliselt.Count);
            Assert.Equal(new int[] { 60, 64, 67 }, arvuliselt[0]);
            Assert.Equal(new int[] { 65, 69, 72 }, arvuliselt[1]);
            Assert.Equal(new int[] { 67, 71, 74 }, arvuliselt[2]);
            Assert.Equal(new int[] { 60, 64, 67 }, arvuliselt[3]);
            
            // Test GetNoodidNimedena
            var nimedena = lugu.GetNoodidNimedena().ToList();
            Assert.Equal(4, nimedena.Count);
            Assert.Equal(new string[] { "C", "E", "G" }, nimedena[0]);
            Assert.Equal(new string[] { "F", "A", "C" }, nimedena[1]);
            Assert.Equal(new string[] { "G", "H", "D" }, nimedena[2]);
            Assert.Equal(new string[] { "C", "E", "G" }, nimedena[3]);
        }

        [Fact]
        public void Lugu_EmptyMeasures()
        {
            var lugu = new Lugu { Nimetus = "Empty Song" };
            
            var arvuliselt = lugu.GetNoodidArvuliselt().ToList();
            Assert.Empty(arvuliselt);
            
            var nimedena = lugu.GetNoodidNimedena().ToList();
            Assert.Empty(nimedena);
        }

        [Fact]
        public void MidiToName_AllNotes()
        {
            // Test all 12 notes in the chromatic scale
            Assert.Equal("C", Kolmkola.MidiToName(60));
            Assert.Equal("C#", Kolmkola.MidiToName(61));
            Assert.Equal("D", Kolmkola.MidiToName(62));
            Assert.Equal("Eb", Kolmkola.MidiToName(63));
            Assert.Equal("E", Kolmkola.MidiToName(64));
            Assert.Equal("F", Kolmkola.MidiToName(65));
            Assert.Equal("F#", Kolmkola.MidiToName(66));
            Assert.Equal("G", Kolmkola.MidiToName(67));
            Assert.Equal("G#", Kolmkola.MidiToName(68));
            Assert.Equal("A", Kolmkola.MidiToName(69));
            Assert.Equal("B", Kolmkola.MidiToName(70));
            Assert.Equal("H", Kolmkola.MidiToName(71));
            // Test wrapping
            Assert.Equal("C", Kolmkola.MidiToName(72)); // Next octave
        }
    }
}
