import { useState } from 'react';
import axios from 'axios';

export default function ChatBox({ currentUser, messages, setMessagess }) {
    const [message, setMessage] = useState('');

    const handleSend = async () => {

        try {
            if (message.trim() === '') return;

            // Aquí enviarías el mensaje al servidor con SignalR o API

            const newMessage = {
                contenido: message,
                id_emisor: currentUser.id_Emisor,
                id_remitente: currentUser.id_remitente,
            }

            // console.log({
            //     contenido: message,
            //     sender: "Yo",
            // })

            // setMessagess(prevMessages => [
            //     ...prevMessages,
            //     {
            //         sender: "Yo",
            //         contenido: message,
            //     }
            // ]);


            const response = await axios.post('https://localhost:7005/api/chat/newMensaje', newMessage,
                currentUser,
                {
                    withCredentials: true
                }
            );

            setMessage('');
        } catch (error) {
            alert("Error al enviar el mensaje");
            setMessage('');

            console.error("Error al enviar el mensaje:", error);
        }

    };

    return (
        <div style={{ padding: '16px', height: '100%', display: 'flex', flexDirection: 'column' }}>
            <h2>Chat con usuario seleccionado</h2>

            <div style={{
                flex: 1,
                border: '1px solid #ddd',
                marginBottom: '10px',
                padding: '10px',
                overflowY: 'auto'
            }}>
                {/* Mensajes irían aquí */}
                {messages && messages.map((msg, index) => {

                    return (
                        <div key={index} style={{ marginBottom: '8px' }}>
                            <strong>{msg.sender}</strong>: {msg.contenido}
                        </div>
                    );
                })}
            </div>

            <div style={{ display: 'flex', gap: '8px' }}>
                <input
                    type="text"
                    value={message}
                    onChange={e => setMessage(e.target.value)}
                    onKeyDown={e => e.key === 'Enter' && handleSend()}
                    placeholder="Escribe un mensaje..."
                    style={{ flex: 1, padding: '8px' }}
                />
                <button onClick={handleSend} style={{ padding: '8px 16px' }}>Enviar</button>
            </div>
        </div>
    );
}
