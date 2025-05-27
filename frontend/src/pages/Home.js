import React from "react";
import AplicacoesList from "../components/AplicacoesList";
import TecnologiaList from "../components/TecnologiaList";
import Formulario from "../components/Formulario";
import "./Home.css"; // 🔥 Arquivo de estilos


const Home = () => {
  return (
    <div className="container">
      <Formulario />
      <div className="tabelas">
        <AplicacoesList />
        <TecnologiaList />
      </div>
    </div>
  );
};

export default Home;
