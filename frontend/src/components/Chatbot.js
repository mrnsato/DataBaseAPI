import React, { useState } from "react";
import axios from "axios";

const Chatbot = () => {
  const [mensagem, setMensagem] = useState("");
  const [resposta, setResposta] = useState("");

  const enviarPergunta = async () => {
    try {
      const response = await axios.post("http://localhost:5115/ia/perguntar", {
        pergunta: mensagem,
      });

      setResposta(response.data); // ✅ Exibir resposta da IA
    } catch (error) {
      console.error("Erro ao obter resposta da IA:", error);
      setResposta("Erro ao comunicar com o chatbot.");
    }
  };

  return (
    <div style={{ padding: "20px", maxWidth: "400px", margin: "auto", textAlign: "center", border: "1px solid #ccc", borderRadius: "10px" }}>
      <h2>Chatbot IA 🤖</h2>
      <input
        type="text"
        value={mensagem}
        onChange={(e) => setMensagem(e.target.value)}
        placeholder="Digite sua pergunta..."
        style={{ width: "80%", padding: "10px", marginBottom: "10px", borderRadius: "5px" }}
      />
      <button onClick={enviarPergunta} style={{ padding: "10px 20px", cursor: "pointer", borderRadius: "5px", backgroundColor: "#007bff", color: "#fff" }}>
        Enviar
      </button>
      <p style={{ marginTop: "20px", fontWeight: "bold" }}>{resposta}</p>
    </div>
  );
};

export default Chatbot;
