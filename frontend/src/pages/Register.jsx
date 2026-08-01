
import { useState } from "react";
import authService from "../services/authService";

function Register({ onRegisterSuccess }) {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");

    try {
      const response = await authService.register(username, password);
      localStorage.setItem("token", response.data.token);
      localStorage.setItem("username", response.data.username);
      onRegisterSuccess();
    } catch (err) {
      setError(err.response?.data?.message || "Qeydiyyat uğursuz oldu.");
    }
  };

  return (
    <div className="min-h-screen flex items-center justify-center px-4">
      <form
        onSubmit={handleSubmit}
        className="w-full max-w-sm bg-[var(--paper-dark)]/50 border border-[var(--line)] rounded-sm p-8"
      >
        <h1 className="font-display text-2xl mb-6 text-center">Qeydiyyat</h1>

        {error && (
          <p className="text-[var(--stamp)] text-sm mb-4 font-mono">{error}</p>
        )}

        <input
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          placeholder="istifadəçi adı"
          className="w-full mb-3 bg-transparent font-mono text-sm border-b border-[var(--line)] focus:border-[var(--ink)] focus:outline-none py-2"
          required
        />

        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          placeholder="şifrə (ən azı 6 simvol)"
          className="w-full mb-6 bg-transparent font-mono text-sm border-b border-[var(--line)] focus:border-[var(--ink)] focus:outline-none py-2"
          required
        />

        <button
          type="submit"
          className="w-full font-display text-sm uppercase tracking-widest border-2 border-[var(--ink)] py-2 hover:bg-[var(--ink)] hover:text-[var(--paper)] transition-colors"
        >
          Qeydiyyatdan keç
        </button>
      </form>
    </div>
  );
}

export default Register;