import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5115",
});

export default api;
