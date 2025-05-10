import ChatLayout from "./components/ChatLayout";

import Login from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Login />
  },
  {
    path: '/chat',
    element: <ChatLayout />
  }

];

export default AppRoutes;
