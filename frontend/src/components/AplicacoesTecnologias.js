import React, { useState, useEffect } from "react"; // ✅ Importa React Hooks
import axios from "axios"; // ✅ Importa Axios


const AplicacoesTecnologias = () => {
const [dados, setDados] = useState([]);

  useEffect(() => {
  const fetchData = async () => {
  try {
    const response = await axios.get("http://localhost:5115/aplicacoes-com-tecnologias");

    // 🔥 Adiciona "Nenhuma tecnologia cadastrada" se a propriedade estiver indefinida
    const dadosCorrigidos = response.data.map(item => ({
      ...item,
      Tecnologias: Array.isArray(item.Tecnologias) && item.Tecnologias.length > 0 
        ? item.Tecnologias 
        : ["Nenhuma tecnologia cadastrada"]
    }));

    setDados(dadosCorrigidos);
  } catch (error) {
    console.error("Erro ao buscar dados:", error);
  }
};

    fetchData();
  }, []);

  return (
    <div style={{ padding: "20px", maxWidth: "600px", margin: "auto" }}>
      <h2>Relação Aplicações & Tecnologias 🚀</h2>
      <ul>
        {dados.map((item, index) => (
          <li key={index}>
            <strong>{item.Aplicacao}:</strong> {item.Tecnologias.join(", ")}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default AplicacoesTecnologias;
