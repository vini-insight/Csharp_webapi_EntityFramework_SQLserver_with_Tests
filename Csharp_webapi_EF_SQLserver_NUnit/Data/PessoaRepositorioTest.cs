using Data;
using NUnit.Framework;
using Models;

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
            CollectionAssert.IsEmpty(PessoaRepositorio.GetListPessoa());
        }

        [Test, Order(2)]
        public void GetPessoaTestNull()
        {
            Assert.Null(PessoaRepositorio.GetPessoa(CPF_1));
        }

        [Test, Order(3)]
        public void InserirNoBancoDadosTest()
        {
            Assert.IsNotNull(PessoaRepositorio.InserirNoBancoDados(p1));
        }

        [Test, Order(4)]
        public void GetListPessoaTest()
        {
            CollectionAssert.IsNotEmpty(PessoaRepositorio.GetListPessoa());
        }

        [Test, Order(5)]
        public void GetPessoaTest()
        {
            Assert.IsNotNull(PessoaRepositorio.GetPessoa(CPF_1));
        }

        [Test, Order(6)]
        public void AtualizarNoBancoDadosTest()
        {
            Assert.IsNotNull(PessoaRepositorio.AtualizarNoBancoDados(p2));
        }

        [Test, Order(7)]
        public void AtualizarNoBancoDadosNullTest()
        {
            Assert.IsNull(PessoaRepositorio.AtualizarNoBancoDados(p3));
        }

        [Test, Order(8)]
        public void ApagarNoBancoDadosNullTest()
        {
            Assert.IsNull(PessoaRepositorio.ApagarNoBancoDados(CPF_2));
        }

        [Test, Order(9)]
        public void ApagarNoBancoDadosTest()
        {
            Assert.IsNotNull(PessoaRepositorio.ApagarNoBancoDados(CPF_1));
        }        

        /*

        [Test]
        public void AtualizarNoBancoDadosTest()
        {
            // const string NOME_1 = "Penelope Charmosa";
            // const string NOME_2 = "Capitão Caverna";        
            const string CPF_1 = "87769269102";
            // const string CPF_2 = "17769269108";
            // const string DATA_1 = "19/07/1998";
            // const string DATA_2 = "10/12/1950";
            const string SEXO_M = "m";
            // const string SEXO_F = "f";
            PessoaRepositorio pr = new PessoaRepositorio(new DataContext());
            // Pessoa p1 = new Pessoa { Nome = NOME_1, Cpf = CPF_1, DataNascimento = DATA_1, Sexo = SEXO_F };
            PessoaPut p2 = new PessoaPut { Nome = "outro nome", Cpf = CPF_1, DataNascimento = "9/2/1397", Sexo = SEXO_M };
            // PessoaPut p3 = new PessoaPut { Nome = NOME_2, Cpf = CPF_2, DataNascimento = DATA_2, Sexo = SEXO_M };

            Assert.IsNotNull(PessoaRepositorio.AtualizarNoBancoDados(p2));
        }

        */
    }
}