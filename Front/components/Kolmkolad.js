import React, { useEffect, useState } from 'react';

const API = "https://localhost:7162/api/kolmkolad"; // muuda vastavalt backend hostile

export default function Kolmkolad(){
  const [kolmkolad, setKolmkolad] = useState([]);
  const [uueTahis, setUueTahis] = useState("");
  const [uuePohitoon, setUuePohitoon] = useState("");

  useEffect(()=> fetchAll(),[]);

  function fetchAll(){
    fetch(API).then(r=>r.json()).then(setKolmkolad);
  }

  function lisa(){
    const body = {};
    if(uueTahis) body.Tahis = uueTahis;
    if(uuePohitoon) body.Pohitoon = parseInt(uuePohitoon);
    fetch(API, {
      method: 'POST',
      headers: {'Content-Type':'application/json'},
      body: JSON.stringify(body)
    }).then(()=> { setUueTahis(""); setUuePohitoon(""); fetchAll(); });
  }

  function kustuta(id){
    if(!window.confirm("Kas kustutada see kolmkõla?")) return;
    fetch(`${API}/${id}`, { method: 'DELETE' }).then(()=>fetchAll());
  }

  function muuda(id){
    const uusTahis = prompt("Sisesta uus tähtnimetus (nt C, G):");
    const uusPohitoon = prompt("Sisesta uus põhitoon (MIDI number):");
    if(uusTahis==null && uusPohitoon==null) return;
    const body = {};
    if(uusTahis) body.Tahis = uusTahis;
    if(uusPohitoon) body.Pohitoon = parseInt(uusPohitoon);
    fetch(`${API}/${id}`, {
      method: 'PUT',
      headers: {'Content-Type':'application/json'},
      body: JSON.stringify(body)
    }).then(()=>fetchAll());
  }

  function showNoodid(id, formaad){
    fetch(`${API}/${id}/noodid?formaat=${formaad}`).then(r=>r.json()).then(d=>{
      alert("Noodid:\n" + JSON.stringify(d));
    });
  }

  return (
    <div>
      <h2>Kolmkõlad</h2>
      <div style={{ marginBottom: 10 }}>
        <input placeholder="Tähtnimetus (nt C)" value={uueTahis} onChange={e=>setUueTahis(e.target.value)} />
        <input placeholder="Põhitoon (MIDI nr)" value={uuePohitoon} onChange={e=>setUuePohitoon(e.target.value)} style={{marginLeft:8}}/>
        <button onClick={lisa} style={{marginLeft:8}}>Lisa</button>
      </div>

      <table border="1" cellPadding="6" style={{ borderCollapse: 'collapse', width: '100%' }}>
        <thead>
          <tr>
            <th>Id</th>
            <th>Täht</th>
            <th>Põhitoon</th>
            <th>Noodid (arvuliselt)</th>
            <th>Toimingud</th>
          </tr>
        </thead>
        <tbody>
          {kolmkolad.map(k => (
            <tr key={k.id}>
              <td>{k.id}</td>
              <td>{k.tahis}</td>
              <td>{k.pohitoon}</td>
              <td>{JSON.stringify([k.pohitoon, k.pohitoon+4, k.pohitoon+7])}</td>
              <td>
                <button onClick={()=>muuda(k.id)}>Muuda</button>
                <button onClick={()=>kustuta(k.id)} style={{marginLeft:6}}>Kustuta</button>
                <button onClick={()=>showNoodid(k.id,'nimed')} style={{marginLeft:6}}>Näita nimega</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
