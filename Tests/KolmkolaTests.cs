using Xunit;
using AkordideApi.Models;

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
        public void NameToMidi_ja_MidiToName()
        {
            Assert.Equal(60, Kolmkola.NameToMidi("C"));
            Assert.Equal("C", Kolmkola.MidiToName(60));
            Assert.Equal("G", Kolmkola.MidiToName(67));
        }

        [Fact]
        public void ConstructByName()
        {
            var g = new Kolmkola("G");
            Assert.Equal(67, g.Pohitoon);
            Assert.Equal(new int[] { 67, 71, 74 }, g.GetNoodid());
        }
    }
}
