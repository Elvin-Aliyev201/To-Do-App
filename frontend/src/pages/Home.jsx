// src/pages/Home.jsx
import { useState, useEffect } from "react";
import todoService from "../services/todoService";
import TodoList from "../components/TodoList";
import TodoForm from "../components/TodoForm";

function Home({username, onLogout}) {
  const [todos, setTodos] = useState([]);

  useEffect(() => {
    todoService.getAll().then((response) => {
      setTodos(response.data);
    });
  }, []);

  const handleCreate = (title) => {
    todoService.create({ title }).then((response) => {
      setTodos([...todos, response.data]);
    });
  };

  const handleDelete = (id) => {
    todoService.remove(id).then(() => {
      setTodos(todos.filter((todo) => todo.id !== id));
    });
  };

  const handleToggle = (todo) => {
    const updatedTodo = { ...todo, isCompleted: !todo.isCompleted };
    todoService.update(todo.id, updatedTodo).then(() => {
      setTodos(todos.map((t) => (t.id === todo.id ? updatedTodo : t)));
    });
  };

  const handleUpdateTitle = (todo, newTitle) => {
    const updatedTodo = { ...todo, title: newTitle };
    todoService.update(todo.id, updatedTodo).then(() => {
      setTodos(todos.map((t) => (t.id === todo.id ? updatedTodo : t)));
    });
  };

  const openCount = todos.filter((t) => !t.isCompleted).length;

 
  return (
    <div className="min-h-screen py-8 sm:py-16 px-4">
      <div className="max-w-2xl mx-auto bg-[var(--paper-dark)]/50 border border-[var(--line)] rounded-sm shadow-[0_10px_30px_-14px_rgba(0,0,0,0.4)] p-5 sm:p-8 md:p-10 sm:-rotate-[0.3deg]">
        <header className="flex flex-wrap items-start justify-between gap-y-3 border-b-2 border-[var(--ink)] pb-4 mb-6">
          <div>
            <h1 className="font-display text-2xl sm:text-3xl tracking-wide">
              TAPŞIRIQ DƏFTƏRİ
            </h1>
            {username && (
              <p className="font-mono text-sm text-[var(--ink-faded)] mt-1">
                sahib: <span className="text-[var(--moss)]">{username}</span>
              </p>
            )}
          </div>

          <div className="flex items-center gap-4">
            <span className="font-mono text-sm text-[var(--ink-faded)] uppercase tracking-widest">
              {openCount} açıq
            </span>
            <button
              onClick={onLogout}
              className="font-mono text-sm uppercase tracking-wide text-[var(--stamp)] hover:underline py-1"
            >
              Çıxış
            </button>
          </div>
        </header>

        <TodoForm onCreate={handleCreate} />

        <TodoList
          todos={todos}
          onDelete={handleDelete}
          onToggle={handleToggle}
          onUpdateTitle={handleUpdateTitle}
        />

        {todos.length === 0 && (
          <p className="text-center text-base text-[var(--ink-faded)] py-10 font-mono">
            Dəftər boşdur. İlk qeydi yaz.
          </p>
        )}
      </div>
    </div>
  );
}

export default Home;