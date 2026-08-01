import api from "../api/axios";

const register = (username, password) =>
  api.post("/Auth/register", { username, password });

const login = (username, password) =>
  api.post("/Auth/login", { username, password });

export default {
  register,
  login,
};