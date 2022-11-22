-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 22-Nov-2022 às 03:29
-- Versão do servidor: 10.4.25-MariaDB
-- versão do PHP: 7.4.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `db_rh`
--

-- --------------------------------------------------------

--
-- Estrutura da tabela `cargo`
--

CREATE TABLE `cargo` (
  `idCargo` int(11) NOT NULL,
  `Descricao` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Extraindo dados da tabela `cargo`
--

INSERT INTO `cargo` (`idCargo`, `Descricao`) VALUES
(1, 'Analista de Sistemas'),
(2, 'Analista de Qualidade');

-- --------------------------------------------------------

--
-- Estrutura da tabela `dadospessoais`
--

CREATE TABLE `dadospessoais` (
  `id` int(11) NOT NULL,
  `idUsuario` int(11) NOT NULL,
  `nomePai` varchar(255) DEFAULT NULL,
  `nomeMae` varchar(255) DEFAULT NULL,
  `idEstadoCivil` tinyint(4) DEFAULT NULL,
  `nomeEsposa` varchar(255) DEFAULT NULL,
  `dataNascimento` date DEFAULT NULL,
  `numeroDependentes` tinyint(4) DEFAULT NULL,
  `naturalidade` varchar(100) DEFAULT NULL,
  `nacionalidade` varchar(100) DEFAULT NULL,
  `idGrauDeEstudo` tinyint(4) DEFAULT NULL,
  `celular` varchar(15) DEFAULT NULL,
  `telefoneFixo` varchar(14) DEFAULT NULL,
  `rg` varchar(12) DEFAULT NULL,
  `dataExpedicao` date DEFAULT NULL,
  `orgaoExpedidor` varchar(10) DEFAULT NULL,
  `certidaoDeReservista` varchar(15) DEFAULT NULL,
  `carteiraDeTrabalho` varchar(12) DEFAULT NULL,
  `carteiraDeTrabalhoSerie` varchar(2) DEFAULT NULL,
  `titulo` varchar(14) DEFAULT NULL,
  `zona` varchar(3) DEFAULT NULL,
  `secao` varchar(4) DEFAULT NULL,
  `idUF` tinyint(4) DEFAULT NULL,
  `cnh` varchar(12) DEFAULT NULL,
  `categoriasCNH` varchar(10) DEFAULT NULL,
  `validadeCNH` date DEFAULT NULL,
  `validadePrimeiraCNH` date DEFAULT NULL,
  `idCargo` int(11) DEFAULT NULL,
  `fotoPerfil` mediumblob DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `empregosantigos`
--

CREATE TABLE `empregosantigos` (
  `id` int(11) NOT NULL,
  `idUsuario` int(11) NOT NULL,
  `empresa` varchar(100) DEFAULT NULL,
  `telefone` varchar(15) DEFAULT NULL,
  `contato` varchar(100) DEFAULT NULL,
  `setor` varchar(100) DEFAULT NULL,
  `cargo` varchar(100) DEFAULT NULL,
  `endereco` varchar(255) DEFAULT NULL,
  `dataAdmissao` date DEFAULT NULL,
  `dataDemissao` date DEFAULT NULL,
  `motivoDemissao` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `endereco`
--

CREATE TABLE `endereco` (
  `id` int(11) NOT NULL,
  `idUsuario` int(11) NOT NULL,
  `rua` varchar(100) DEFAULT NULL,
  `numero` varchar(10) DEFAULT NULL,
  `bairro` varchar(100) DEFAULT NULL,
  `cep` varchar(10) DEFAULT NULL,
  `cidade` varchar(100) DEFAULT NULL,
  `estado` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `estadocivil`
--

CREATE TABLE `estadocivil` (
  `id` tinyint(4) NOT NULL,
  `descricaoEstadoCivil` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `estadocivil`
--

INSERT INTO `estadocivil` (`id`, `descricaoEstadoCivil`) VALUES
(2, 'Casado'),
(1, 'Solteiro');

-- --------------------------------------------------------

--
-- Estrutura da tabela `formularios`
--

CREATE TABLE `formularios` (
  `id` int(11) NOT NULL,
  `idUsuario` int(11) NOT NULL,
  `nomeVaga` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `graudeestudo`
--

CREATE TABLE `graudeestudo` (
  `id` tinyint(4) NOT NULL,
  `descricaoGrauDeEstudo` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `graudeestudo`
--

INSERT INTO `graudeestudo` (`id`, `descricaoGrauDeEstudo`) VALUES
(1, 'Ensino Superior');

-- --------------------------------------------------------

--
-- Estrutura da tabela `perguntas`
--

CREATE TABLE `perguntas` (
  `idPergunta` int(11) NOT NULL,
  `descricao` varchar(255) NOT NULL,
  `peso` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `perguntausuario`
--

CREATE TABLE `perguntausuario` (
  `id` int(11) NOT NULL,
  `idPergunta` int(11) NOT NULL,
  `idUsuario` int(11) NOT NULL,
  `repostaAlternativa` tinyint(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estrutura da tabela `uf`
--

CREATE TABLE `uf` (
  `id` int(11) NOT NULL,
  `uf` varchar(2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Extraindo dados da tabela `uf`
--

INSERT INTO `uf` (`id`, `uf`) VALUES
(1, 'AC'),
(2, 'AL'),
(3, 'AM'),
(4, 'AP'),
(5, 'BA'),
(6, 'CE'),
(7, 'DF'),
(8, 'ES'),
(9, 'GO'),
(10, 'MA'),
(11, 'MG'),
(12, 'MS'),
(13, 'MT'),
(14, 'PA'),
(15, 'PB'),
(16, 'PE'),
(17, 'PI'),
(18, 'PR'),
(19, 'RJ'),
(20, 'RN'),
(21, 'RO'),
(22, 'RR'),
(23, 'RS'),
(24, 'SC'),
(25, 'SE'),
(26, 'SP'),
(27, 'TO'),
(99, 'EX');

-- --------------------------------------------------------

--
-- Estrutura da tabela `usuario`
--

CREATE TABLE `usuario` (
  `id` int(11) NOT NULL,
  `cpf` varchar(14) NOT NULL,
  `nome` varchar(255) NOT NULL,
  `email` varchar(255) NOT NULL,
  `senha` varchar(255) NOT NULL,
  `admin` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Índices para tabelas despejadas
--

--
-- Índices para tabela `cargo`
--
ALTER TABLE `cargo`
  ADD PRIMARY KEY (`idCargo`);

--
-- Índices para tabela `dadospessoais`
--
ALTER TABLE `dadospessoais`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idUsuario` (`idUsuario`),
  ADD KEY `idEstadoCivil` (`idEstadoCivil`),
  ADD KEY `idGrauDeEstudo` (`idGrauDeEstudo`);

--
-- Índices para tabela `empregosantigos`
--
ALTER TABLE `empregosantigos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idUsuario` (`idUsuario`);

--
-- Índices para tabela `endereco`
--
ALTER TABLE `endereco`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `idUsuario` (`idUsuario`,`cep`);

--
-- Índices para tabela `estadocivil`
--
ALTER TABLE `estadocivil`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `descricaoEstadoCivil` (`descricaoEstadoCivil`);

--
-- Índices para tabela `formularios`
--
ALTER TABLE `formularios`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idUsuario` (`idUsuario`);

--
-- Índices para tabela `graudeestudo`
--
ALTER TABLE `graudeestudo`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `descricaoGrauDeEstudo` (`descricaoGrauDeEstudo`);

--
-- Índices para tabela `perguntas`
--
ALTER TABLE `perguntas`
  ADD PRIMARY KEY (`idPergunta`);

--
-- Índices para tabela `perguntausuario`
--
ALTER TABLE `perguntausuario`
  ADD PRIMARY KEY (`id`),
  ADD KEY `FK_Pergunta_idx` (`idPergunta`),
  ADD KEY `perguntasUsuario_ibfk_1_idx` (`idUsuario`);

--
-- Índices para tabela `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `cpf` (`cpf`);

--
-- AUTO_INCREMENT de tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `cargo`
--
ALTER TABLE `cargo`
  MODIFY `idCargo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `dadospessoais`
--
ALTER TABLE `dadospessoais`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `empregosantigos`
--
ALTER TABLE `empregosantigos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `endereco`
--
ALTER TABLE `endereco`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de tabela `estadocivil`
--
ALTER TABLE `estadocivil`
  MODIFY `id` tinyint(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `formularios`
--
ALTER TABLE `formularios`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `graudeestudo`
--
ALTER TABLE `graudeestudo`
  MODIFY `id` tinyint(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de tabela `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Restrições para despejos de tabelas
--

--
-- Limitadores para a tabela `dadospessoais`
--
ALTER TABLE `dadospessoais`
  ADD CONSTRAINT `dadosPessoais_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`id`),
  ADD CONSTRAINT `dadosPessoais_ibfk_2` FOREIGN KEY (`idEstadoCivil`) REFERENCES `estadocivil` (`id`),
  ADD CONSTRAINT `dadosPessoais_ibfk_3` FOREIGN KEY (`idGrauDeEstudo`) REFERENCES `graudeestudo` (`id`);

--
-- Limitadores para a tabela `empregosantigos`
--
ALTER TABLE `empregosantigos`
  ADD CONSTRAINT `empregosAntigos_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`id`);

--
-- Limitadores para a tabela `endereco`
--
ALTER TABLE `endereco`
  ADD CONSTRAINT `endereco_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`id`);

--
-- Limitadores para a tabela `formularios`
--
ALTER TABLE `formularios`
  ADD CONSTRAINT `formularios_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`id`);

--
-- Limitadores para a tabela `perguntausuario`
--
ALTER TABLE `perguntausuario`
  ADD CONSTRAINT `perguntasUsuario_ibfk_1` FOREIGN KEY (`idUsuario`) REFERENCES `usuario` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `perguntasUsuario_ibfk_2` FOREIGN KEY (`idPergunta`) REFERENCES `perguntas` (`idPergunta`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
