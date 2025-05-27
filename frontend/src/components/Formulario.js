import React, { useState } from "react";
import api from "../services/api";

const Formulario = () => {
  const [nomeAplicacao, setNomeAplicacao] = useState("");
  const [nomeTecnologia, setNomeTecnologia] = useState("");
  const [tipo, setTipo] = useState("aplicacao"); // Alterna entre Aplicação e Tecnologia
  const [nome, setNome] = useState(""); // Para armazenar o nome do item a ser cadastrado

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (tipo === "aplicacao") {
        await api.post("http://localhost:5115/aplicacoes", { nome: nomeAplicacao });
      } else {
        await api.post("http://localhost:5115/tecnologia", { nome: nomeTecnologia });
      }
      alert("Cadastrado com sucesso!");
      setNomeAplicacao("");
      setNomeTecnologia("");
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
          placeholder={tipo === "aplicacao" ? "Nome da Aplicação" : "Nome da Tecnologia"}
          value={tipo === "aplicacao" ? nomeAplicacao : nomeTecnologia}
          onChange={(e) => tipo === "aplicacao" ? setNomeAplicacao(e.target.value) : setNomeTecnologia(e.target.value)}
        />
        <button type="submit">Cadastrar</button>
      </form>
    </div>
  );
};

export default Formulario;
