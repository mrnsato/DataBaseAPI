import React, { useState } from "react";
import api from "../services/api";

const Formulario = () => {
  const [tipo, setTipo] = useState("aplicacao"); // Alternar entre Aplicação e Tecnologia
  const [nome, setNome] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await api.post(`/${tipo}`, { nome }); // Envia para aplicação ou tecnologia conforme o tipo
      alert("Cadastrado com sucesso!");
      setNome(""); // Limpa o campo após o cadastro
    } catch (error) {
      console.error("Erro ao cadastrar:", error);
    }
  };

  return (
    <div>
      <h2>Cadastro</h2>
      <select onChange={(e) => setTipo(e.target.value)} value={tipo}>
        <option value="aplicacao">Aplicação</option>
        <option value="tecnologia">Tecnologia</option>
      </select>

      <form onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
        />
        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
};

export default Formulario;
