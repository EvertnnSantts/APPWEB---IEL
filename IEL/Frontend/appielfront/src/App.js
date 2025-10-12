import './App.css';
import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalHeader, ModalFooter } from 'reactstrap';

function App() {
  const baseUrl = "https://localhost:44369/api/Aluno";
  const [data, setData] = useState([]);
  const [erro, setErro] = useState('');

  // 🔹 Carrega os dados do backend
  const pedirDados = async () => {
    try {
      const response = await axios.get(baseUrl);
      setData(response.data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    pedirDados();
  }, []);

  // 🔹 Estado do aluno selecionado
  const [alunoSelecionado, setAlunoSelecionado] = useState({
    id: '',
    name: '',
    email: '',
    dateConclusao: '',
    address: ''
  });

  // 🔹 Modal de inclusão
  const [modalIncluir, setModalIncluir] = useState(false);
  const abrirFecharModalIncluir = () => {
    setErro('');
    setModalIncluir(!modalIncluir);
  };

  // 🔹 Modal de edição
  const [modalEditar, setModalEditar] = useState(false);
  const abrirFecharModalEditar = () => {
    setModalEditar(!modalEditar);
  };

  // 🔹 Atualiza campos do aluno
  const handleChange = (e) => {
    const { name, value } = e.target;
    setAlunoSelecionado((prev) => ({
      ...prev,
      [name]: value
    }));
  };

  // 🔹 Seleciona aluno para editar ou excluir
  const selecionarAluno = (aluno, opcao) => {
    setAlunoSelecionado(aluno);
    opcao === "Editar" ? abrirFecharModalEditar() : abrirFecharModalIncluir();
  };

  // 🔹 Salva novo aluno
  const pedidoPost = async () => {
    const { name, email, dateConclusao, address } = alunoSelecionado;

    if (!name.trim() || !email.trim() || !dateConclusao.trim() || !address.trim()) {
      setErro('Por favor, preencha todos os campos antes de salvar.');
      return;
    }

    const novoAluno = { ...alunoSelecionado };
    delete novoAluno.id;

    try {
      const response = await axios.post(baseUrl, novoAluno);
      setData([...data, response.data]);
      abrirFecharModalIncluir();
      setAlunoSelecionado({
        id: '',
        name: '',
        email: '',
        dateConclusao: '',
        address: ''
      });
    } catch (error) {
      console.log(error);
    }
  };

  // 🔹 Atualiza aluno existente
  const pedidoPut = async () => {
    try {
      const alunoAtualizado = {
        ...alunoSelecionado,
        dateConclusao: new Date(alunoSelecionado.dateConclusao).toISOString()
      };
    
      const response = await axios.put(`${baseUrl}/${alunoSelecionado.id}`, alunoAtualizado);

      setData(data.map((aluno) =>
        aluno.id === alunoSelecionado.id ? response.data : aluno
      ));

      setModalEditar(false);
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <div className="App container">
      <br />
      <h2>Cadastro de Alunos</h2>

      <header className="App-header mb-3">
        <button className="btn btn-primary" onClick={abrirFecharModalIncluir}>
          Adicionar Novo Aluno
        </button>
      </header>

      <table className="table table-bordered">
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>E-mail</th>
            <th>Data de Conclusão</th>
            <th>Endereço</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {data.map((aluno) => (
            <tr key={aluno.id}>
              <td>{aluno.id}</td>
              <td>{aluno.name ?? aluno.Name}</td>
              <td>{aluno.email ?? aluno.Email}</td>
              <td>{aluno.dateConclusao?.split('T')[0] ?? aluno.DateConclusao}</td>
              <td>{aluno.address ?? aluno.Address}</td>
              <td>
                <button
                  className="btn btn-primary btn-sm"
                  onClick={() => selecionarAluno(aluno, "Editar")}
                >
                  Editar
                </button>{" "}
                <button
                  className="btn btn-danger btn-sm"
                  onClick={() => selecionarAluno(aluno, "Excluir")}
                >
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* 🔹 Modal de inclusão */}
      <Modal isOpen={modalIncluir} toggle={abrirFecharModalIncluir}>
        <ModalHeader toggle={abrirFecharModalIncluir}>
          Adicionar novo Aluno
        </ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nome</label>
            <input
              type="text"
              className="form-control"
              name="name"
              value={alunoSelecionado.name}
              onChange={handleChange}
            />

            <label>E-mail</label>
            <input
              type="text"
              className="form-control"
              name="email"
              value={alunoSelecionado.email}
              onChange={handleChange}
            />
            
            <label>Data de Conclusão</label>
            <input
              type="date"
              className="form-control"
              name="dateConclusao"
              value={alunoSelecionado.dateConclusao}
              onChange={handleChange}
            />

            <label>Endereço</label>
            <input
              type="text"
              className="form-control"
              name="address"
              value={alunoSelecionado.address}
              onChange={handleChange}
            />

            {erro && <div className="alert alert-danger mt-3">{erro}</div>}
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={pedidoPost}>
            Salvar
          </button>
          <button className="btn btn-danger" onClick={abrirFecharModalIncluir}>
            Cancelar
          </button>
        </ModalFooter>
      </Modal>

      {/* 🔹 Modal de edição */}
      <Modal isOpen={modalEditar}>
        <ModalHeader>Editar Aluno</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>ID</label>
            <input
              type="text"
              className="form-control"
              name="id"
              readOnly
              value={alunoSelecionado?.id || ''}
            />
            <br />

            <label>Nome</label>
            <input
              type="text"
              className="form-control"
              name="name"
              value={alunoSelecionado?.name || ''}
              onChange={handleChange}
            />
            <br />

            <label>E-mail</label>
            <input
              type="text"
              className="form-control"
              name="email"
              value={alunoSelecionado?.email || ''}
              onChange={handleChange}
            />
            <br />

            <label>Data de Conclusão</label>
            <input
              type="date"
              className="form-control"
              name="dateConclusao"
              value={alunoSelecionado?.dateConclusao?.split('T')[0] || ''}
              onChange={handleChange}
            />
            <br />

            <label>Endereço</label>
            <input
              type="text"
              className="form-control"
              name="address"
              value={alunoSelecionado?.address || ''}
              onChange={handleChange}
            />
          </div>
        </ModalBody>

        <ModalFooter>
          <button className="btn btn-primary" onClick={pedidoPut}>
            Salvar
          </button>
          <button
            className="btn btn-danger"
            onClick={() => setModalEditar(false)}
          >
            Cancelar
          </button>
        </ModalFooter>
      </Modal>
    </div>
  );
}

export default App;
