import { useState, useEffect } from "react";
import todoService from "../services/todoService";

function Home() {
  const [todos, setTodos] = useState([]);

  useEffect(() => {
    todoService.getAll().then((response) => {
      setTodos(response.data);
    });
  }, []);

  return (
    <div className="max-w-xl mx-auto mt-10 px-4">
      <h1 className="text-2xl font-bold mb-4">Todo App</h1>

      <ul className="space-y-2">
        {todos.map((todo) => (
          <li key={todo.id} className="border rounded p-2">
            {todo.title}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default Home;