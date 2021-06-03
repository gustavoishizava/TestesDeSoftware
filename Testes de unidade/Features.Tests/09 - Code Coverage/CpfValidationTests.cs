using Features.Core;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Features.Tests._09___Code_Coverage
{
    public class CpfValidationTests
    {
        [Theory(DisplayName = "CPF Validos")]
        [Trait("Categoria", "CPF Validation Tests")]
        [InlineData("17630624069")]
        [InlineData("18642052023")]
        [InlineData("64184957307")]
        [InlineData("21681764423")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerValidos(string cpf)
        {
            // Arrange
            var cpfValidation = new CpfValidation();

            // Act & Assert
            cpfValidation.EhValido(cpf).Should().BeTrue();
        }

        [Theory(DisplayName = "CPF Invalidos")]
        [Trait("Categoria", "CPF Validation Tests")]
        [InlineData("17630624010")]
        [InlineData("18642052025")]
        [InlineData("64184957308")]
        [InlineData("21681764426")]
        public void Cpf_ValidarMultiplosNumeros_TodosDevemSerInvalidos(string cpf)
        {
            // Arrange
            var cpfValidation = new CpfValidation();

            // Act & Assert
            cpfValidation.EhValido(cpf).Should().BeFalse();
        }
    }
}
