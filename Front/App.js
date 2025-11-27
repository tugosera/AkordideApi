import React from 'react';
import Kolmkolad from './components/Kolmkolad';
import Lood from './components/Lood';

function App() {
  return (
    <div style={{ padding: 20, fontFamily: 'Arial, sans-serif' }}>
      <h1>Akordide haldus</h1>
      <p>See rakendus võimaldab lisada, muuta ja kustutada kolmkõlasid ning luua lugusid.</p>
      <div style={{ display: 'flex', gap: 40 }}>
        <div style={{ flex: 1 }}>
          <Kolmkolad />
        </div>
        <div style={{ flex: 1 }}>
          <Lood />
        </div>
      </div>
    </div>
  );
}

export default App;
