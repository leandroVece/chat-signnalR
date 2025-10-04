# Chat Simple con SignalR

Este proyecto es un **chat básico** desarrollado con fines prácticos y educativos. Utiliza **SignalR** para la comunicación en tiempo real entre usuarios, **SQLite** como base de datos y almacena la sesión del usuario en el **localStorage** del navegador.

## Funcionamiento

- **Inicio de sesión:**  
  El usuario ingresa su nombre y contraseña. Si las credenciales son correctas, la sesión se guarda en el localStorage.

- **Listado de usuarios:**  
  Se muestra una lista de usuarios disponibles para chatear, excluyendo al usuario actual.

- **Chat en tiempo real:**  
  Al seleccionar un usuario, se inicia una conversación. Los mensajes se envían y reciben en tiempo real usando SignalR.

- **Persistencia:**  
  Todos los mensajes y conversaciones se almacenan en una base de datos SQLite.

## Tecnologías

- **Frontend:** React
- **Backend:** ASP.NET Core
- **Comunicación en tiempo real:** SignalR
- **Base de datos:** SQLite
- **Gestión de sesión:** localStorage

## Nota

Este chat está pensado para **fines prácticos** y de aprendizaje. No implementa seguridad avanzada ni cifrado de datos.

---

Para más detalles revisa los componentes en [`src/components`](src/components).