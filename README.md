# chat-signnalR

Este proyecto es una aplicación de chat desarrollada con fines de prueba.

## Tecnologías utilizadas
- ASP.NET Core
- SignalR
- Entity Framework Core
- React (cliente)
- SQLite (base de datos)

## Funcionalidad de Login
El sistema de login almacena el estado del usuario en el localStorage del navegador.

## Estructura principal

### Controladores (`Controllers`)
- **ChatController**: Gestiona las operaciones principales del chat, como el inicio de sesión, obtención de usuarios, recuperación y envío de mensajes. Utiliza SignalR para la comunicación en tiempo real entre los usuarios.

### Componentes (`ClientApp/src/components`)
- **ChatLayout**: Organiza la interfaz principal del chat, mostrando la lista de usuarios y el área de conversación.
- **ChatBox**: Permite enviar y visualizar mensajes en la conversación activa.
- **NavMenu**: Barra de navegación para acceder a las distintas vistas (login, chat).
- **Home**: Componente de inicio de sesión, guarda el usuario autenticado en localStorage.

---
Este proyecto es solo para propósitos de aprendizaje y experimentación.
