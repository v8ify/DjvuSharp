using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DjvuSharp.LispExpressions;
using NUnit.Framework;

namespace DjvuSharp.Tests
{
    [TestFixture]
    public class TestLispExpression
    {
        private readonly double _floatValue = 44.798;
        private readonly int _intValue = 1000;
        private readonly string _stringValue = "This is a string expression";
        private readonly string _symbolValue = "A new symbol";

        private FloatExpression _floatExpression;
        private IntExpression _intExpression;
        private StringExpression _stringExpression;
        private ListExpression _listExpression;
        private Symbol _symbol;

        [OneTimeSetUp]
        public void SetUp()
        {
            _floatExpression = new FloatExpression(_floatValue);
            _intExpression = new IntExpression(_intValue);
            _stringExpression = new StringExpression(_stringValue);
            _listExpression = new ListExpression(_floatExpression, new ListExpression(_intExpression, new ListExpression(_intExpression, _stringExpression)));
            _symbol = new Symbol(_symbolValue);
        }

        [Test]
        public void Test_Float_Expression_Value()
        {
            Assert.AreEqual(_floatValue, _floatExpression.Value);
        }

        [Test]
        public void Test_Int_Expression_Value()
        {
            Assert.AreEqual(_intValue, _intExpression.Value);
        }

        [Test]
        public void Test_String_Expression_Value()
        {
            Assert.AreEqual(_stringValue, _stringExpression.Value);
        }

        [Test]
        public void Test_Symbol_Value()
        {
            Assert.AreEqual(_symbolValue, _symbol.Name);
        }

        [Test]
        public void Test_ListExpression_Length()
        {
            Assert.AreEqual(3, _listExpression.Length);
        }
    }
}
