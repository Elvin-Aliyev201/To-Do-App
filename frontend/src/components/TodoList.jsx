// src/components/TodoList.jsx
import TodoItem from "./TodoItem";

function TodoList({ todos, onDelete, onToggle, onUpdateTitle }) {
  return (
    <ul>
      {todos.map((todo, index) => (
        <TodoItem
          key={todo.id}
          entryNumber={index + 1}
          todo={todo}
          onDelete={onDelete}
          onToggle={onToggle}
          onUpdateTitle={onUpdateTitle}
        />
      ))}
    </ul>
  );
}

export default TodoList;