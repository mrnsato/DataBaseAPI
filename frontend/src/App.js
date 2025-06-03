import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import Home from "./pages/Home";
import Chatbot from "./components/Chatbot"; // ✅ Importa o Chatbot

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/chatbot" element={<Chatbot />} /> {/* 🔥 Adiciona o Chatbot na rota */}
      </Routes>
    </Router>
  );
}

export default App;
