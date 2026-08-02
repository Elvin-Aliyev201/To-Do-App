// src/components/TodoItem.jsx
import { useState } from "react";

function TodoItem({ todo, entryNumber, onDelete, onToggle, onUpdateTitle }) {
  const [isEditing, setIsEditing] = useState(false);
  const [editTitle, setEditTitle] = useState(todo.title);

  const handleSave = () => {
    if (!editTitle.trim()) return;
    onUpdateTitle(todo, editTitle);
    setIsEditing(false);
  };

  const handleCancel = () => {
    setEditTitle(todo.title);
    setIsEditing(false);
  };

 return (
  <li className="grid grid-cols-[1.75rem_auto_1fr_auto] sm:grid-cols-[2.5rem_auto_1fr_auto] items-center gap-2 sm:gap-3 py-2.5 border-b border-dotted border-[var(--line)] relative">
    <span className="font-mono text-[10px] sm:text-xs text-[var(--ink-faded)]">
      {String(entryNumber).padStart(3, "0")}
    </span>

    <input
      type="checkbox"
      checked={todo.isCompleted}
      onChange={() => onToggle(todo)}
      className="accent-[#566B4A] w-4 h-4 shrink-0"
    />

    <div className="relative min-w-0">
      {isEditing ? (
        <input
          type="text"
          value={editTitle}
          onChange={(e) => setEditTitle(e.target.value)}
          className="w-full bg-transparent font-mono text-sm border-b border-[var(--ink)] focus:outline-none"
          autoFocus
        />
      ) : (
        <span
          onDoubleClick={() => setIsEditing(true)}
          className={`font-mono text-sm cursor-text break-words ${
            todo.isCompleted ? "line-through text-[var(--ink-faded)]" : ""
          }`}
        >
          {todo.title}
        </span>
      )}

      {todo.isCompleted && (
        <span className="stamp absolute -top-3 left-1/4 sm:left-1/3 rotate-[-8deg] border-2 border-[var(--stamp)] text-[var(--stamp)] text-[9px] sm:text-[10px] font-display px-1.5 leading-tight pointer-events-none select-none">
          BİTDİ
        </span>
      )}
    </div>

    {isEditing ? (
      <div className="flex flex-col sm:flex-row gap-1 sm:gap-2 text-[10px] sm:text-xs font-display uppercase tracking-wide shrink-0">
        <button onClick={handleSave} className="text-[var(--moss)]">Saxla</button>
        <button onClick={handleCancel} className="text-[var(--ink-faded)]">Ləğv et</button>
      </div>
    ) : (
      <button
        onClick={() => onDelete(todo.id)}
        className="text-[10px] sm:text-xs font-display uppercase tracking-wide text-[var(--stamp)] hover:underline shrink-0"
      >
        Sil
      </button>
    )}
  </li>
);
}

export default TodoItem;