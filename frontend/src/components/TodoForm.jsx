// src/components/TodoForm.jsx
import { useState } from "react";

function TodoForm({ onCreate }) {
  const [title, setTitle] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!title.trim()) return;
    onCreate(title);
    setTitle("");
  };

  return (
    <form onSubmit={handleSubmit} className="flex items-center gap-4 pb-5 mb-3 border-b border-[var(--line)]">
      <span className="font-display text-[var(--ink-faded)] text-lg">+</span>

      <input
        type="text"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        placeholder="yeni tapşırıq yaz..."
        className="flex-1 bg-transparent font-mono text-base placeholder:text-[var(--ink-faded)] focus:outline-none border-b border-dashed border-[var(--line)] focus:border-[var(--ink)] py-1.5"
      />

      <button
        type="submit"
        className="relative font-display text-sm uppercase tracking-widest text-[var(--stamp)] border-2 border-[var(--stamp)] px-4 py-2 rotate-[-2deg]
                   hover:bg-[var(--stamp)] hover:text-[var(--paper)] active:scale-90 active:rotate-0
                   transition-all duration-150"
      >
        Yaz
        <span className="absolute inset-0 border border-[var(--stamp)] m-0.5 pointer-events-none" />
      </button>
    </form>
  );
}

export default TodoForm;