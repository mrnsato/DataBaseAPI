import React, { useEffect, useState } from "react";
import api from "../services/api";

const TecnologiaList = () => {
  const [tecnologias, setTecnologias] = useState([]);

  useEffect(() => {
    api.get("http://localhost:5115/tecnologia") // Ajuste conforme o endpoint da sua API
      .then(response => setTecnologias(response.data))
      .catch(error => console.error("Erro ao buscar tecnologias:", error));
  }, []);

  return (
    <div>
      <h2>Lista de Tecnologias</h2>
      <ul>
        {tecnologias.map(tec => (
          <li key={tec.id}>{tec.nome}</li>
        ))}
      </ul>
    </div>
  );
};

export default TecnologiaList;
