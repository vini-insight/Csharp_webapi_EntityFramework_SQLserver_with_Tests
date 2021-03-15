using System;
using Data;
using Models;
using NUnit.Framework;
using FluentAssertions;
using Bogus;
using Bogus.Extensions.Brazil;

namespace Data
{
    [TestFixture]
    public class PessoaRepositórioTest
    {
        private PessoaRepositorio pr { get; set; }        
        private string[] generos = new string[2] { "M", "F" };
        private Pessoa p1, p1S; // p1S para conseguir recuperar mesma informação no BD
        private PessoaPut p2;
        private PessoaPut p3;

        [SetUp]
        public void Setup()
        {
            pr = new PessoaRepositorio(new DataContext());
            p1 = new Faker<Pessoa>("pt_BR")                                                
                                            .RuleFor(p => p.Nome, f => f.Person.FullName)
                                            .RuleFor(p => p.Cpf, f => f.Person.Cpf())
                                            .RuleFor(p => p.DataNascimento, f => GetRandomizerDate())
                                            .RuleFor(p => p.Sexo, f => f.PickRandom(generos));
            p1.Cpf = p1.Cpf.Replace(".", ""); // adequar ao CPF gerado pelo Bogus para o formato de dado que eu espero
            p1.Cpf = p1.Cpf.Replace("-", ""); // adequar ao CPF gerado pelo Bogus para o formato de dado que eu espero
            p2 = new Faker<PessoaPut>("pt_BR")                                                
                                            .RuleFor(p => p.Nome, f => f.Person.FullName)
                                            .RuleFor(p => p.Cpf, f => f.Person.Cpf())
                                            .RuleFor(p => p.DataNascimento, f => GetRandomizerDate())
                                            .RuleFor(p => p.Sexo, f => f.PickRandom(generos));
            p2.Cpf = p2.Cpf.Replace(".", ""); // adequar ao CPF gerado pelo Bogus para o formato de dado que eu espero
            p2.Cpf = p2.Cpf.Replace("-", ""); // adequar ao CPF gerado pelo Bogus para o formato de dado que eu espero
            p3 = new Faker<PessoaPut>("pt_BR")                                                
                                            .RuleFor(p => p.Nome, f => f.Person.FullName)
                                            .RuleFor(p => p.Cpf, f => f.Person.Cpf())
                                            .RuleFor(p => p.DataNascimento, f => GetRandomizerDate())
                                            .RuleFor(p => p.Sexo, f => f.PickRandom(generos));
            p3.Cpf = p3.Cpf.Replace(".", ""); // adequar ao CPF gerado pelo Bogus para o formato de dado que eu espero
            p3.Cpf = p3.Cpf.Replace("-", ""); // adequar ao CPF gerado pelo Bogus para o formato de dado que eu espero
        }

        [Test, Order(1)]
        public void GetListPessoaVaziaTest()
        {
            PessoaRepositorio.GetListPessoa().Should().BeEmpty();
        }

        [Test, Order(2)]
        public void GetPessoaTestNull()
        {
            PessoaRepositorio.GetPessoa(p1.Cpf).Should().BeNull();
        }

        [Test, Order(3)]
        public void InserirNoBancoDadosTest()
        {
            p1S = p1;
            ShowResults(p1);
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
            ShowResults(p1S);
            PessoaRepositorio.GetPessoa(p1S.Cpf).Should().NotBeNull();
        }
        
        [Test, Order(6)]
        public void AtualizarNoBancoDadosTest()
        {
            p2.Cpf = p1S.Cpf;
            ShowResults(p2);
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
            PessoaRepositorio.ApagarNoBancoDados(p2.Cpf).Should().BeNull();
        }

        [Test, Order(9)]
        public void ApagarNoBancoDadosTest()
        {
            PessoaRepositorio.ApagarNoBancoDados(p1S.Cpf).Should().NotBeNull();
        }
        private string GetRandomizerDate() // feito para adequar a data gerada pelo Bogus ao meu formato de data esperado
        {
            DateTime localDate = DateTime.Now;
            var dia = new Bogus.Randomizer().Number(1,28);
            var mes = new Bogus.Randomizer().Number(1,12);
            var ano = new Bogus.Randomizer().Number(1900, localDate.Year);
            return ""+ dia +"/" + mes + "/" + ano;
        }

        private void ShowResults(Object o) // Console.WriteLine("message"); não funciona em modo teste no VScode por isso criei esse método para me auxiliar no debug
        {
            if (typeof(Pessoa).IsInstanceOfType(o))
            {
                Pessoa p = (Pessoa) o;
                NUnit.Framework.TestContext.Progress.WriteLine(p.Nome);
                NUnit.Framework.TestContext.Progress.WriteLine(p.Cpf);
                NUnit.Framework.TestContext.Progress.WriteLine(p.DataNascimento);
                NUnit.Framework.TestContext.Progress.WriteLine(p.Sexo);
            }
            else if (typeof(PessoaPut).IsInstanceOfType(o))
            {
                PessoaPut t = (PessoaPut) o;
                NUnit.Framework.TestContext.Progress.WriteLine("PESSOA PUT");
                NUnit.Framework.TestContext.Progress.WriteLine(t.Nome);
                NUnit.Framework.TestContext.Progress.WriteLine(t.Cpf);
                NUnit.Framework.TestContext.Progress.WriteLine(t.DataNascimento);
                NUnit.Framework.TestContext.Progress.WriteLine(t.Sexo);
            }
            else
            {
                NUnit.Framework.TestContext.Progress.WriteLine("TIPO ERRADO");
            }
        }
    }
}