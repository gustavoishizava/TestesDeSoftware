using Xunit;

namespace Features.Tests._08___Skip
{
    public class TesteNaoPassandoPorMotivoEspecifico
    {
        [Fact(DisplayName = "Novo cliente 2.0", Skip = "Nova versão 2.0 quebrando")]
        [Trait("Categoria", "Escapando dos testes")]
        public void Teste_NaoEstaPassando_VersaoNovaNaoCompativel()
        {
            Assert.True(false);
        }
    }
}
