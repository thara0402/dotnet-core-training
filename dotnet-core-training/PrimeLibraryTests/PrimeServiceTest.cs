﻿using PrimeLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PrimeLibrary.Tests
{
    public class PrimeServiceTest
    {
        private readonly PrimeService _service;

        public PrimeServiceTest()
        {
            _service = new PrimeService();
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            var result = _service.IsPrime(value);
            Assert.False(result, $"{value} should not be prime");
        }

        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        public void IsPrime_NonPrimesLessThan10_ReturnFalse(int value)
        {
            var result = _service.IsPrime(value);
            Assert.False(result, $"{value} should not be prime");
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        public void IsPrime_PrimesLessThan10_ReturnTrue(int value)
        {
            var result = _service.IsPrime(value);
            Assert.True(result, $"{value} should be prime");
        }
    }
}
