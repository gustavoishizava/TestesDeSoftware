using System;
using System.Linq;
using Xunit;
namespace NerdStore.Vendas.Domain.Tests
{
    public class VoucherTests
    {
        [Fact(DisplayName = "Validar voucher tipo valor válido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("CPD123", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(1), true, false);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar voucher tipo valor inválido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoValor_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, TipoDescontoVoucher.Valor, 0, DateTime.Now.AddDays(-1), false, true);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.ValorDescontoErroMsg, result.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "Validar voucher tipo porcentabem válido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoPorcentagem_DeveEstarValido()
        {
            // Arrange
            var voucher = new Voucher("CPD123", 15, null, TipoDescontoVoucher.Porcentagem, 1, DateTime.Now.AddDays(1), true, false);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Validar voucher tipo porcentagem inválido")]
        [Trait("Categoria", "Vendas - Voucher")]
        public void Voucher_ValidarVoucherTipoPorcentagem_DeveEstarInvalido()
        {
            // Arrange
            var voucher = new Voucher("", null, null, TipoDescontoVoucher.Porcentagem, 0, DateTime.Now.AddDays(-1), false, true);

            // Act
            var result = voucher.ValidarSeAplicavel();

            // Assert
            Assert.False(result.IsValid);
            Assert.Equal(6, result.Errors.Count);
            Assert.Contains(VoucherAplicavelValidation.AtivoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.CodigoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.DataValidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.QuantidadeErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.UtilizadoErroMsg, result.Errors.Select(x => x.ErrorMessage));
            Assert.Contains(VoucherAplicavelValidation.PercentualDescontoErroMsg, result.Errors.Select(x => x.ErrorMessage));
        }
    }
}
