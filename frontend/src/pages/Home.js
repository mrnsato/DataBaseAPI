import React from "react";
import AplicacoesList from "../components/AplicacoesList";
import TecnologiaList from "../components/TecnologiaList";
import Formulario from "../components/Formulario";

const Home = () => {
  return (
    <div>
      <h1>Gestão de Aplicações e Tecnologias</h1>
      <Formulario />
      <AplicacoesList />
      <TecnologiaList />
    </div>
  );
};

export default Home;
