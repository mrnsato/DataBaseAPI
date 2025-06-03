import React, { useEffect, useState } from "react";
import api from "../services/api";

const AplicacoesList = () => {
  const [aplicacoes, setAplicacoes] = useState([]);
  const [editando, setEditando] = useState(null);
  const [novoNome, setNovoNome] = useState("");

  useEffect(() => {
    buscarAplicacoes();
  }, []);

  const buscarAplicacoes = async () => {
    try {
      const response = await api.get("http://localhost:5115/aplicacoes"); // 🔥 Confirme que esse é o endpoint correto
      setAplicacoes(response.data);
    } catch (error) {
      console.error("Erro ao buscar aplicações:", error);
    }
  };

  const deletarAplicacao = async (id) => {
    try {
      await api.delete(`http://localhost:5115/aplicacoes/${id}`); // 🔥 Rota DELETE
      buscarAplicacoes();
    } catch (error) {
      console.error("Erro ao deletar aplicação:", error);
    }
  };

  const iniciarEdicao = (id, nomeAtual) => {
    setEditando(id);
    setNovoNome(nomeAtual);
  };

  const atualizarAplicacao = async (id) => {
    try {
      await api.put(`http://localhost:5115/aplicacoes/${id}`, { nome: novoNome }); // 🔥 Rota PUT
      setEditando(null);
      setNovoNome("");
      buscarAplicacoes();
    } catch (error) {
      console.error("Erro ao atualizar aplicação:", error);
    }
  };

  return (
    <div>
      <h2>Lista de Aplicações</h2>
      <ul>
        {aplicacoes.map((app) => (
          <li key={app.id}>
            {editando === app.id ? (
              <>
                <input
                  type="text"
                  value={novoNome}
                  onChange={(e) => setNovoNome(e.target.value)}
                />
                <button onClick={() => atualizarAplicacao(app.id)}>Salvar</button>
                <button onClick={() => setEditando(null)}>Cancelar</button>
              </>
            ) : (
              <>
                {app.nome}
                <button onClick={() => iniciarEdicao(app.id, app.nome)}>✏️ Editar</button>
                <button onClick={() => deletarAplicacao(app.id)}>❌ Excluir</button>
              </>
            )}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default AplicacoesList;
