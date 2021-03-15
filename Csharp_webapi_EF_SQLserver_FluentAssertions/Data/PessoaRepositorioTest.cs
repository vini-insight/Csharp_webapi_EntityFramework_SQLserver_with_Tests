using Data;
using Models;
using NUnit.Framework;
using FluentAssertions;

namespace Data
{
    [TestFixture]
    public class PessoaRepositorioTest
    {
        private PessoaRepositorio pr { get; set; }        
        const string NOME_1 = "Penelope Charmosa";
        const string NOME_2 = "Capitão Caverna";        
        const string CPF_1 = "87769269102";
        const string CPF_2 = "17769269108";
        const string DATA_1 = "19/07/1998";
        const string DATA_2 = "10/12/1950";
        const string SEXO_M = "m";
        const string SEXO_F = "f";
        private Pessoa p1;
        private PessoaPut p2;
        private PessoaPut p3;

        [SetUp]
        public void Setup()
        {
            pr = new PessoaRepositorio(new DataContext());
            p1 = new Pessoa { Nome = NOME_1, Cpf = CPF_1, DataNascimento = DATA_1, Sexo = SEXO_F };
            p2 = new PessoaPut { Nome = "outro nome", Cpf = CPF_1, DataNascimento = "9/2/1397", Sexo = SEXO_M };
            p3 = new PessoaPut { Nome = NOME_2, Cpf = CPF_2, DataNascimento = DATA_2, Sexo = SEXO_M };
        }

        [Test, Order(1)]
        public void GetListPessoaVaziaTest()
        {
            PessoaRepositorio.GetListPessoa().Should().BeEmpty();
        }

        [Test, Order(2)]
        public void GetPessoaTestNull()
        {
            PessoaRepositorio.GetPessoa(CPF_1).Should().BeNull();
        }

        [Test, Order(3)]
        public void InserirNoBancoDadosTest()
        {
            PessoaRepositorio.InserirNoBancoDados(p1).Should().NotBeNull();
        }
        
        [Test, Order(4)]
        public void GetListPessoaTest()
        {
            PessoaRepositorio.GetListPessoa().Should().NotBeEmpty();
        }

        [Test, Order(5)]
        public void GetPessoaTest()
        {
            PessoaRepositorio.GetPessoa(CPF_1).Should().NotBeNull();
        }
        
        [Test, Order(6)]
        public void AtualizarNoBancoDadosTest()
        {
            PessoaRepositorio.AtualizarNoBancoDados(p2).Should().NotBeNull();
        }

        [Test, Order(7)]
        public void AtualizarNoBancoDadosNullTest()
        {
            PessoaRepositorio.AtualizarNoBancoDados(p3).Should().BeNull();
        }

        [Test, Order(8)]
        public void ApagarNoBancoDadosNullTest()
        {
            PessoaRepositorio.ApagarNoBancoDados(CPF_2).Should().BeNull();
        }

        [Test, Order(9)]
        public void ApagarNoBancoDadosTest()
        {
            PessoaRepositorio.ApagarNoBancoDados(CPF_1).Should().NotBeNull();
        }
    }
}