import React from "react";
import AplicacoesList from "../components/AplicacoesList";
import TecnologiaList from "../components/TecnologiaList";
import Formulario from "../components/Formulario";
import Chatbot from "../components/Chatbot"; // ✅ Importa o Chatbot
import "./Home.css"; // 🔥 Arquivo de estilos

const Home = () => {
  return (
    <div className="container">
      <Formulario />
      <div className="tabelas">
        <AplicacoesList />
        <TecnologiaList />
      </div>

      {/* 🔥 Adiciona o Chatbot abaixo das tabelas */}
      <div className="chatbot-container">
        <Chatbot />
      </div>
    </div>
  );
};

export default Home;
