import React from "react";
import AplicacoesList from "../components/AplicacoesList";
import TecnologiaList from "../components/TecnologiaList";
import Formulario from "../components/Formulario";
import AplicacoesTecnologias from "../components/AplicacoesTecnologias"; // ✅ Importa a lista
import "./Home.css";

const Home = () => {
  return (
    <div className="container">
      <Formulario />
      <div className="tabelas">
        <AplicacoesList />
        <TecnologiaList />
      </div>

      {/* 🔥 Exibe Aplicações e suas Tecnologias */}
      <AplicacoesTecnologias />
    </div>
  );
};

export default Home;
