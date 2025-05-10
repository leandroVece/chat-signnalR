import { useState } from 'react';
import axios from 'axios';

export default function Login({ onLoginSuccess }) {
  const [nombre, setNombre] = useState('');
  const [contra, setContra] = useState('');
  const [error, setError] = useState('');

  const handleLogin = async (e) => {
    e.preventDefault();
    setError('');

    try {
      const res = await axios.post('https://localhost:7005/api/chat/login', { nombre, contra },
        {
          withCredentials: true
        }
      );
      const userData = res.data;

      // Guardar en localStorage
      localStorage.setItem('user', JSON.stringify(userData));
      // Notificar al padre o cambiar estado global
      // onLoginSuccess(userData);
    } catch (err) {
      setError('Credenciales inválidas');
    }


  };

  return (
    <div>
      <h2>Iniciar sesión</h2>
      <form onSubmit={handleLogin}>
        <input type="text" placeholder="Usuario" value={nombre} onChange={(e) => setNombre(e.target.value)} />
        <input type="password" placeholder="Contraseña" value={contra} onChange={(e) => setContra(e.target.value)} />
        <button type="submit">Ingresar</button>
        {error && <p style={{ color: 'red' }}>{error}</p>}
      </form>
    </div>
  );
}
