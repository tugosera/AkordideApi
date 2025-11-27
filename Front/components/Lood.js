import React, { useEffect, useState } from 'react';

const API = "https://localhost:7162/api/lood";

export default function Lood(){
  const [lood, setLood] = useState([]);
  const [nimi, setNimi] = useState("");
  const [taktTahis, setTaktTahis] = useState("");

  useEffect(()=> load(), []);

  function load(){ fetch(API).then(r=>r.json()).then(setLood); }

  async function lisaLugu(){
    const lugu = { Nimetus: nimi, Taktid: [] };
    const res = await fetch(API, { method: 'POST', headers: {'Content-Type':'application/json'}, body: JSON.stringify(lugu) });
    setNimi("");
    load();
  }

  async function lisaTakt(luguId){
    // Lisame lihtsa takti, kus on ühe kolmkõla Tahis
    const tahis = prompt("Sisesta takti akordi tähtnimetus (nt C, G, F):", "C");
    if(!tahis) return;
    // Laeme lugu, lisame takti ja PUT-ime
    const lugu = await (await fetch(`${API}/${luguId}`)).json();
    if(!lugu.taktid) lugu.taktid = [];
    // takti objekt: Kolmkola with Tahis and Pohitoon inferred
    const uusTakt = { Positsioon: lugu.taktid.length + 1, Kolmkola: { Tahis: tahis } };
    lugu.taktid.push(uusTakt);
    await fetch(`${API}/${luguId}`, { method: 'PUT', headers: {'Content-Type':'application/json'}, body: JSON.stringify(lugu) });
    load();
  }

  function kustutaLugu(id){
    if(!window.confirm("Kas kustutada lugu?")) return;
    fetch(`${API}/${id}`, { method: 'DELETE' }).then(()=>load());
  }

  function näitaNoodid(id, formaad){
    fetch(`${API}/${id}/noodid?formaat=${formaad}`).then(r=>r.json()).then(d=>{
      alert(JSON.stringify(d));
    });
  }

  return (
    <div>
      <h2>Luugud</h2>
      <div style={{ marginBottom: 10 }}>
        <input placeholder="Loo nimetus" value={nimi} onChange={e=>setNimi(e.target.value)} />
        <button onClick={lisaLugu} style={{ marginLeft: 8 }}>Lisa lugu</button>
      </div>

      <ul>
        {lood.map(l => (
          <li key={l.id} style={{ marginBottom: 8 }}>
            <strong>{l.nimetus}</strong> (Id: {l.id}) &nbsp;
            <button onClick={()=>lisaTakt(l.id)}>Lisa takt</button>
            <button onClick={()=>näitaNoodid(l.id,'arv')} style={{marginLeft:8}}>Näita noodid (arv)</button>
            <button onClick={()=>näitaNoodid(l.id,'nimed')} style={{marginLeft:8}}>Näita noodid (nimed)</button>
            <button onClick={()=>kustutaLugu(l.id)} style={{marginLeft:8}}>Kustuta</button>
            <div style={{ marginTop:6 }}>
              <em>Takid:</em>
              <ol>
                {(l.taktid || []).map(t => (
                  <li key={t.id || Math.random()}>
                    {t.kolmkola?.tahis} ({t.kolmkola?.pohitoon})
                  </li>
                ))}
              </ol>
            </div>
          </li>
        ))}
      </ul>
    </div>
  );
}
