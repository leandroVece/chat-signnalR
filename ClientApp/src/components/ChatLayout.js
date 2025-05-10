import UserList from './UserList';
import ChatBox from './ChatBox';
import axios from 'axios';
import { HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useState } from 'react';

const conversacionObj = {
  id_Emisor: 0,
  id_remitente: 0,
}

const mensajeObj = {
  id_mensaje: 0,
  contenido: ""
}

export default function ChatLayout() {
  const [currentUser, setCurrentUser] = useState(conversacionObj);
  const [users, setUsers] = useState([]);
  const [db, setdb] = useState([]);
  const [messagess, setMessagess] = useState([]);
  const [contact, setContact] = useState({});
  const [hubConnection, setHubConnection] = useState();



  const handleUserSelect = (user) => {
    console.log("Usuario seleccionado:", user.id_user);
    if (user.IdConversacion === null) {
      user.IdConversacion = 0;
    }
    setCurrentUser({
      id_remitente: user.id_user,
      id_Emisor: db.id_user,
    });

    setContact(user);
    // Aquí podrías guardar en estado el usuario seleccionado para chatear
  };

  const fetchMessaje = async () => {
    try {
      const response = await axios.post('https://localhost:7005/api/chat/AllMesaje', currentUser,
        currentUser,
        {
          withCredentials: true
        }
      );

      var aux = response.data.map((msg) => {
        if (msg.id_emisor == contact.id_user) {
          return {
            sender: contact.name,
            contenido: msg.contenido,
          }
        }
        else {
          return {
            sender: "Yo",
            contenido: msg.contenido,
          }
        }
      })
      console.log(aux);
      setMessagess(aux);
    } catch (error) {
      setMessagess(null);

      console.error("Error al obtener los mensajes:", error);
    }
  };

  const fetchUsers = async () => {
    try {
      const user = JSON.parse(localStorage.getItem('user'));
      setdb(user);

      var response = axios.get(`https://localhost:7005/api/chat/all/${user.id_user}`) // Asegúrate de tener este endpoint
        .then(res => setUsers(res.data))
        .catch(err => console.error(err));

      setUsers(response.data);
    }
    catch (error) {

      console.error("Error al obtener los usuarios:", error);
    }
  }

  useEffect(() => {
    fetchUsers();

  }, []);

  useEffect(() => {

    fetchMessaje();
  }, [currentUser])


  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl("https://localhost:7005/chatHub")
      .withAutomaticReconnect()
      .build();

    connect
      .start()
      .then(() => {
        connect.on("ReceiveMessage", (mensaje) => {
          setMessagess(prev => [
            ...prev,
            {
              sender: mensaje.id_emisor === contact.id_user ? contact.name : "Yo",
              contenido: mensaje.contenido
            }
          ]);
        });
      })
      .catch(err => console.error("Error al conectar con SignalR:", err));

    return () => {
      connect.stop(); // Limpiar conexión al desmontar
    };
  }, [currentUser]);

  // Escuchar mensajes en función del contacto
  useEffect(() => {
    const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:7005/chatHub")
      .withAutomaticReconnect()
      .build();

    connection
      .start()
      .then(() => {
        console.log("Conectado a SignalR");

        // Guardamos la conexión en estado para otros efectos si es necesario
        setHubConnection(connection);

        // Escuchamos los mensajes
        connection.on("ReceiveMessage", (mensaje) => {
          const ids = [contact.id_user, currentUser.id_user];

          // Verificamos si el mensaje corresponde a la conversación actual
          if (ids.includes(mensaje.id_emisor) && ids.includes(mensaje.id_remitente)) {
            setMessagess(prev => [
              ...prev,
              {
                sender: mensaje.id_emisor === contact.id_user ? contact.name : "Yo",
                contenido: mensaje.contenido
              }
            ]);
          }
        });
      })
      .catch(err => console.error("Error al conectar con SignalR:", err));

    return () => {
      connection.stop();
    };
  }, [currentUser, contact]);



  return (
    <div className="chat-container">
      <aside className="sidebar">
        <h3>Usuarios</h3>
        <UserList onUserSelect={handleUserSelect}
          users={users} />
      </aside>
      <main className="chat-main">
        <ChatBox currentUser={currentUser}
          messages={messagess}
          setMessagess={setMessagess} />
      </main>
    </div>
  );
}
