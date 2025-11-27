using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AkordideApi.Models
{
    // Põhi-Kolmkõla klass
    public class Kolmkola
    {
        [Key]
        public int Id { get; set; }

        // Põhitoon MIDI numbrina (täisarv)
        public ICollection<Takt> Taktid { get; set; } = new List<Takt>();

        public int Pohitoon { get; set; }

        // Valik: salvestada ka akordi tähis (C, F, G jne)
        public string? Tahis { get; set; }

        public Kolmkola() { }

        // Konstruktor, kui antakse põhitoon
        public Kolmkola(int pohitoon)
        {
            Pohitoon = pohitoon;
            Tahis = MidiToName(pohitoon);
        }

        // Konstruktor, kui antakse tähtnimetus (nt "C", "G", "F")
        public Kolmkola(string tahis)
        {
            Tahis = tahis;
            Pohitoon = NameToMidi(tahis);
        }

        // Tagastab triadi noodid arvuliste MIDI numbritena: põhitoon, suur terts (+4), kvint (+7)
        public virtual int[] GetNoodid()
        {
            return new[] { Pohitoon, Pohitoon + 4, Pohitoon + 7 };
        }

        // Tagastab noodid tähtnimedena
        public string[] GetNimed()
        {
            return GetNoodid().Select(n => MidiToName(n)).ToArray();
        }

        // Muudab MIDI numbri täheks
        public static string MidiToName(int midi)
        {
            // Alates 60 = C, skaala ümberringi
            string[] names = { "C", "C#", "D", "Eb", "E", "F", "F#", "G", "G#", "A", "B", "H" };
            int offset = midi - 60;
            int idx = ((offset % 12) + 12) % 12;
            return names[idx];
        }

        // Muudab akordi tähtnimetuse MIDI põhitooniks (võtab esimesel korrusel sobiva numbri >=60)
        public static int NameToMidi(string nimi)
        {
            string n = nimi.Trim().ToUpperInvariant();
            var map = new Dictionary<string, int>
            {
                {"C", 60}, {"C#", 61}, {"DB", 61}, {"D", 62}, {"D#", 63}, {"EB", 63},
                {"E", 64}, {"F", 65}, {"F#", 66}, {"GB", 66}, {"G", 67}, {"G#", 68},
                {"AB", 68}, {"A", 69}, {"A#", 70}, {"BB", 70}, {"B", 71}, {"H", 71}
            };

            if (map.ContainsKey(n))
                return map[n];

            // Kui on täiendav oktav määratud nagu C4, H jne, lihtsustame: võta esimene täht ja tagasta vastav 60+ offset
            if (n.Length > 0)
            {
                string key = n.Substring(0, Math.Min(2, n.Length));
                if (map.ContainsKey(key))
                    return map[key];
            }

            throw new ArgumentException($"Tundmatu noodinimi: {nimi}");
        }
    }
}
