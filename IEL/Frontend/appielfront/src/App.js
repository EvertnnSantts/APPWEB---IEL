import './App.css';
import React, { useState, useEffect } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import { Modal, ModalBody, ModalHeader, ModalFooter } from 'reactstrap';
import { NumericFormat } from 'react-number-format';

function App() {
  const baseUrl = "https://localhost:44369/api/Aluno";
  const [data, setData] = useState([]);
  const [erro, setErro] = useState('');
  const [cpfBusca, setCpfBusca] = useState('');

  // 🔹 Carrega os dados
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

  const [alunoSelecionado, setAlunoSelecionado] = useState({
    id: '',
    name: '',
    email: '',
    dateConclusao: '',
    address: '',
    cpf: ''
  });

  const [modalIncluir, setModalIncluir] = useState(false);
  const [modalEditar, setModalEditar] = useState(false);
  const [modalExcluir, setModalExcluir] = useState(false);

  const abrirFecharModalIncluir = () => {
    setErro('');
    setModalIncluir(!modalIncluir);
  };
  const abrirFecharModalEditar = () => setModalEditar(!modalEditar);
  const abrirFecharModalExcluir = () => setModalExcluir(!modalExcluir);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setAlunoSelecionado((prev) => ({
      ...prev,
      [name]: value
    }));
  };

  const selecionarAluno = (aluno, opcao) => {
    setAlunoSelecionado(aluno);
    (opcao === "Editar") ? abrirFecharModalEditar() : abrirFecharModalExcluir();
  };

  // 🔹 Criar novo aluno
  const pedidoPost = async () => {
    const { name, email, dateConclusao, address, cpf } = alunoSelecionado;
    if (!name.trim() || !email.trim() || !dateConclusao.trim() || !address.trim() || !cpf.trim()) {
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
        id: '', name: '', email: '', dateConclusao: '', address: '', cpf: ''
      });
    } catch (error) {
      console.log(error);
    }
  };

  // 🔹 Atualizar aluno
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

  // 🔹 Deletar aluno
  const pedidoDelete = async () => {
    await axios.delete(`${baseUrl}/${alunoSelecionado.id}`)
      .then(response => {
        setData(data.filter(aluno => aluno.id !== alunoSelecionado.id));
        abrirFecharModalExcluir();
      }).catch(error => console.log(error));
  };

  // 🔹 Buscar aluno por CPF
  const buscarPorCpf = async () => {
    if (!cpfBusca.trim()) {
      pedirDados();
      return;
    }

    try {
      const response = await axios.get(`${baseUrl}/buscar-por-cpf?cpf=${cpfBusca}`);
      if (response.data) {
        setData([response.data]);
      } else {
        setData([]);
      }
    } catch (error) {
      console.log(error);
      setData([]);
    }
  };

  return (
    <div className="Appcontainer">
      <div>
        <h1 className="mt-3">Gerenciamento de Alunos - IEL </h1>
        <hr />
      </div>

      {/* 🔹 Filtro de CPF */}
      <div className="mb-3 d-flex">
        <input
          type="text"
          className="form-control me-2"
          placeholder="Buscar por CPF (so no numero)"
          value={cpfBusca}
          onChange={(e) => setCpfBusca(e.target.value)}
        />
        <button className="btn btn-secondary" onClick={buscarPorCpf}>Buscar</button>
        <button className="btn btn-outline-dark ms-2" onClick={pedirDados}>Limpar</button>
      </div>

      <button className="btn btn-success mb-3" onClick={abrirFecharModalIncluir}>
        Adicionar Novo Aluno
      </button>

      <table className="table table-hover">
        <thead>
          <tr className='table-info'>
            <th>ID</th>
            <th>Nome</th>
            <th>E-mail</th>
            <th>CPF</th>
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
              <td>{aluno.cpf ?? aluno.Cpf}</td>
              <td>{aluno.dateConclusao?.split('T')[0] ?? aluno.DateConclusao}</td>
              <td>{aluno.address ?? aluno.Address}</td>
              <td>
                <button className="btn btn-primary btn-sm me-2" onClick={() => selecionarAluno(aluno, "Editar")}>
                  Editar
                </button>
                <button className="btn btn-danger btn-sm" onClick={() => selecionarAluno(aluno, "Excluir")}>
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>

      {/* 🔹 Modal de inclusão */}
      <Modal isOpen={modalIncluir} toggle={abrirFecharModalIncluir}>
        <ModalHeader toggle={abrirFecharModalIncluir}>Adicionar novo Aluno</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>Nome</label>
            <input type="text" className="form-control" name="name" value={alunoSelecionado.name} onChange={handleChange} />

            <label>E-mail</label>
            <input type="text" className="form-control" name="email" value={alunoSelecionado.email} onChange={handleChange} />

            <label>CPF</label>
           <NumericFormat
           format="###.###.###-##"
           mask="_"
          value={alunoSelecionado.cpf || ''}
          onValueChange={(values) => {
          setAlunoSelecionado((prev) => ({ ...prev, cpf: values.value })); 
        }}
          className="form-control"
         placeholder="000.000.000-00"/>

            <label>Data de Conclusão</label>
            <input type="date" className="form-control" name="dateConclusao" value={alunoSelecionado.dateConclusao} onChange={handleChange} />

            <label>Endereço</label>
            <input type="text" className="form-control" name="address" value={alunoSelecionado.address} onChange={handleChange} />

            {erro && <div className="alert alert-danger mt-3">{erro}</div>}
          </div>
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-primary" onClick={pedidoPost}>Salvar</button>
          <button className="btn btn-danger" onClick={abrirFecharModalIncluir}>Cancelar</button>
        </ModalFooter>
      </Modal>

      {/* 🔹 Modal de edição */}
      <Modal isOpen={modalEditar}>
        <ModalHeader>Editar Aluno</ModalHeader>
        <ModalBody>
          <div className="form-group">
            <label>ID</label>
            <input type="text" className="form-control" name="id" readOnly value={alunoSelecionado?.id || ''} />

            <label>Nome</label>
            <input type="text" className="form-control" name="name" value={alunoSelecionado?.name || ''} onChange={handleChange} />

            <label>E-mail</label>
            <input type="text" className="form-control" name="email" value={alunoSelecionado?.email || ''} onChange={handleChange} />

            <label>CPF</label>
            <input type="text" className="form-control" name="cpf" value={alunoSelecionado?.cpf || ''} onChange={handleChange} />

            <label>Data de Conclusão</label>
            <input type="date" className="form-control" name="dateConclusao" value={alunoSelecionado?.dateConclusao?.split('T')[0] || ''} onChange={handleChange} />

            <label>Endereço</label>
            <input type="text" className="form-control" name="address" value={alunoSelecionado?.address || ''} onChange={handleChange} />
          </div>
        </ModalBody>

        <ModalFooter>
          <button className="btn btn-primary" onClick={pedidoPut}>Salvar</button>
          <button className="btn btn-danger" onClick={() => setModalEditar(false)}>Cancelar</button>
        </ModalFooter>
      </Modal>

      {/* 🔹 Modal de exclusão */}
      <Modal isOpen={modalExcluir}>
        <ModalBody>
          Confirma a exclusão deste aluno <strong>{alunoSelecionado && alunoSelecionado.name}</strong>?
        </ModalBody>
        <ModalFooter>
          <button className="btn btn-danger" onClick={pedidoDelete}>Sim</button>
          <button className="btn btn-secondary" onClick={abrirFecharModalExcluir}>Não</button>
        </ModalFooter>
      </Modal>
    </div>
  );
}

export default App;
