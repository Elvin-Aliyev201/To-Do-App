// src/App.jsx
import { useState } from "react";
import Home from "./pages/Home";
import Login from "./pages/Login";
import Register from "./pages/Register";

function App() {
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [username, setUsername] = useState(localStorage.getItem("username"));
  const [view, setView] = useState("login");

  const handleAuthSuccess = () => {
    setToken(localStorage.getItem("token"));
    setUsername(localStorage.getItem("username"));
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    setToken(null);
    setUsername(null);
  };

  if (!token) {
    return view === "login" ? (
      <div>
        <Login onLoginSuccess={handleAuthSuccess} />
        <p className="text-center font-mono text-sm text-[var(--ink-faded)] px-4">
          Hesabın yoxdur?{" "}
          <button onClick={() => setView("register")} className="underline text-[var(--ink)]">
            Qeydiyyatdan keç
          </button>
        </p>
      </div>
    ) : (
      <div>
        <Register onRegisterSuccess={handleAuthSuccess} />
        <p className="text-center font-mono text-sm text-[var(--ink-faded)] px-4">
          Artıq hesabın var?{" "}
          <button onClick={() => setView("login")} className="underline text-[var(--ink)]">
            Daxil ol
          </button>
        </p>
      </div>
    );
  }

  return <Home username={username} onLogout={handleLogout} />;
}

export default App;