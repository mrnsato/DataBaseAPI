import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5115/aplicacao-tecnologia", // Substitua pelo endpoint da sua API
});

export default api;
