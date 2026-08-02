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
  <li className="grid grid-cols-[2rem_auto_1fr_auto] items-center gap-3 py-3 border-b border-dotted border-[var(--line)] relative">
    <span className="font-mono text-xs text-[var(--ink-faded)]">
      {String(entryNumber).padStart(3, "0")}
    </span>

    <input
      type="checkbox"
      checked={todo.isCompleted}
      onChange={() => onToggle(todo)}
      className="accent-[#566B4A] w-5 h-5 shrink-0"
    />

    <div className="relative min-w-0">
      {isEditing ? (
        <input
          type="text"
          value={editTitle}
          onChange={(e) => setEditTitle(e.target.value)}
          className="w-full bg-transparent font-mono text-base border-b border-[var(--ink)] focus:outline-none py-1"
          autoFocus
        />
      ) : (
        <span
          onDoubleClick={() => setIsEditing(true)}
          className={`font-mono text-base cursor-text break-words ${
            todo.isCompleted ? "line-through text-[var(--ink-faded)]" : ""
          }`}
        >
          {todo.title}
        </span>
      )}

      {todo.isCompleted && (
        <span className="stamp absolute -top-3 left-1/4 rotate-[-8deg] border-2 border-[var(--stamp)] text-[var(--stamp)] text-[10px] font-display px-1.5 leading-tight pointer-events-none select-none">
          BİTDİ
        </span>
      )}
    </div>

    {isEditing ? (
      <div className="flex flex-col gap-1 text-xs font-display uppercase tracking-wide shrink-0">
        <button onClick={handleSave} className="text-[var(--moss)] py-1">Saxla</button>
        <button onClick={handleCancel} className="text-[var(--ink-faded)] py-1">Ləğv et</button>
      </div>
    ) : (
      <button
        onClick={() => onDelete(todo.id)}
        className="text-xs font-display uppercase tracking-wide text-[var(--stamp)] hover:underline py-2 px-1 shrink-0"
      >
        Sil
      </button>
    )}
  </li>
);
}

export default TodoItem;