import api from "../api/axios";

const getAll = () => api.get("/Todo");

const create = (todo) => api.post("/Todo", todo);

const update = (id, todo) => api.put(`/Todo/${id}`, todo);

const remove = (id) => api.delete(`/Todo/${id}`);

export default {
  getAll,
  create,
  update,
  remove,
};