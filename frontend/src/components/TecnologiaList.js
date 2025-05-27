import React, { useEffect, useState } from "react";
import api from "../services/api";

const TecnologiaList = () => {
  const [tecnologias, setTecnologias] = useState([]);
  const [editando, setEditando] = useState(null);
  const [novoNome, setNovoNome] = useState("");

  useEffect(() => {
    buscarTecnologias();
  }, []);

  const buscarTecnologias = async () => {
    try {
      const response = await api.get("http://localhost:5115/tecnologia");
      setTecnologias(response.data);
    } catch (error) {
      console.error("Erro ao buscar tecnologias:", error);
    }
  };

  const deletarTecnologia = async (id) => {
    try {
      await api.delete(`http://localhost:5115/tecnologia/${id}`); // 🔥 Rota DELETE
      buscarTecnologias(); // Atualiza a lista
    } catch (error) {
      console.error("Erro ao deletar tecnologia:", error);
    }
  };

  const iniciarEdicao = (id, nomeAtual) => {
    setEditando(id);
    setNovoNome(nomeAtual);
  };

  const atualizarTecnologia = async (id) => {
    try {
      await api.put(`http://localhost:5115/tecnologia/${id}`, { nome: novoNome }); // 🔥 Rota PUT
      setEditando(null);
      setNovoNome("");
      buscarTecnologias();
    } catch (error) {
      console.error("Erro ao atualizar tecnologia:", error);
    }
  };

  return (
    <div>
      <h2>Lista de Tecnologias</h2>
      <ul>
        {tecnologias.map((tec) => (
          <li key={tec.id}>
            {editando === tec.id ? (
              <>
                <input
                  type="text"
                  value={novoNome}
                  onChange={(e) => setNovoNome(e.target.value)}
                />
                <button onClick={() => atualizarTecnologia(tec.id)}>Salvar</button>
                <button onClick={() => setEditando(null)}>Cancelar</button>
              </>
            ) : (
              <>
                {tec.nome}
                <button onClick={() => iniciarEdicao(tec.id, tec.nome)}>✏️ Editar</button>
                <button onClick={() => deletarTecnologia(tec.id)}>❌ Excluir</button>
              </>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default TecnologiaList;
