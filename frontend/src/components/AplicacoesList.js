import React, { useEffect, useState } from "react";
import api from "../services/api";

const AplicacoesList = () => {
  const [aplicacoes, setAplicacoes] = useState([]);

  useEffect(() => {
    api.get("http://localhost:5115/aplicacoes")
      .then(response => setAplicacoes(response.data))
      .catch(error => console.error("Erro ao buscar aplicações:", error));
  }, []);

  return (
    <div>
      <h2>Lista de Aplicações</h2>
      <ul>
        {aplicacoes.map(app => (
          <li key={app.id}>{app.nome}</li>
        ))}
      </ul>
    </div>
  );
};

export default AplicacoesList;
