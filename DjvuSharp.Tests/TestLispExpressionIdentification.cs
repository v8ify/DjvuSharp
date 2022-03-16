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
    public class TestLispExpressionIdentification
    {
        private FloatExpression _floatExpression;
        private IntExpression _intExpression;
        private ListExpression _listExpression;
        private StringExpression _stringExpression;
        private Symbol _symbol;

        [OneTimeSetUp]
        public void SetUp()
        {
            _floatExpression = new FloatExpression(44.798);
            _intExpression = new IntExpression(1000);
            _listExpression = new ListExpression(_floatExpression, _intExpression);
            _stringExpression = new StringExpression("This is a string expression");
            _symbol = new Symbol("A new symbol");
        }

        [Test]
        public void Check_Valid_Float_Expression()
        {
            Assert.IsTrue(_floatExpression.IsFloatExpression());

            Assert.IsFalse(_floatExpression.IsIntExpression());
            Assert.IsFalse(_floatExpression.IsStringExpression());
            Assert.IsFalse(_floatExpression.IsListExpression());
            Assert.IsFalse(_floatExpression.IsSymbol());
        }

        [Test]
        public void Check_Valid_Int_Expression()
        {
            Assert.IsTrue(_intExpression.IsIntExpression());

            Assert.IsFalse(_intExpression.IsFloatExpression());
            Assert.IsFalse(_intExpression.IsStringExpression());
            Assert.IsFalse(_intExpression.IsListExpression());
            Assert.IsFalse(_intExpression.IsSymbol());
        }

        [Test]
        public void Check_Valid_List_Expression()
        {
            Assert.IsTrue(_listExpression.IsListExpression());

            Assert.IsFalse(_listExpression.IsIntExpression());
            Assert.IsFalse(_listExpression.IsFloatExpression());
            Assert.IsFalse(_listExpression.IsStringExpression());
            Assert.IsFalse(_listExpression.IsSymbol());
        }

        [Test]
        public void Check_Valid_String_Expression()
        {
            Assert.IsTrue(_stringExpression.IsStringExpression());

            Assert.IsFalse(_stringExpression.IsIntExpression());
            Assert.IsFalse(_stringExpression.IsFloatExpression());
            Assert.IsFalse(_stringExpression.IsListExpression());
            Assert.IsFalse(_stringExpression.IsSymbol());
        }

        [Test]
        public void Check_Valid_Symbol()
        {
            Assert.IsTrue(_symbol.IsSymbol());

            Assert.IsFalse(_symbol.IsIntExpression());
            Assert.IsFalse(_symbol.IsFloatExpression());
            Assert.IsFalse(_symbol.IsListExpression());
            Assert.IsFalse(_symbol.IsStringExpression());
        }
    }
}
