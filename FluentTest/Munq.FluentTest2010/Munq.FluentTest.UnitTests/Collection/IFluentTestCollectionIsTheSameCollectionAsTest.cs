﻿#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using System.Collections.Generic;

namespace Munq.FluentTest.UnitTests
{  
    /// <summary>
    ///This is a test class for IFluentTestTest and is intended
    ///to contain all IFluentTestTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IFluentTestCollectionIsTheSameCollectionAsTest
    {

        #region IsTheSameCollectionAs
        [TestMethod]
        public void IsSameCollectionPassesIfSameCollection()
        {
            var testCollection = new List<string>();
            Verify.That(testCollection).IsACollectionThat().IsTheSameCollectionAs(testCollection);          
        }
        
        [TestMethod]
        public void IsSameCollectionFailsIfNull()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                ()=>Verify.That(testCollection).IsACollectionThat().IsTheSameCollectionAs(null)
            ).AndHasAMessageThat().Contains("] can not be compared to [null]");
        }
        
        [TestMethod]
        public void IsSameCollectionFailsIfDifferent()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().IsTheSameCollectionAs(new List<string>())
            ).AndHasAMessageThat().Contains("] should be the same collection as");
        }

        #endregion

        #region IsNotTheSameCollectionAs
        [TestMethod]
        public void IsNotSameCollectionPassesIfDifferent()
        {
            var testCollection = new List<string>();
            Verify.That(testCollection).IsACollectionThat().IsNotTheSameCollectionAs(new List<string>());
        }

        [TestMethod]
        public void IsNotSameCollectionFailsIfNull()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().IsNotTheSameCollectionAs(null)
            ).AndHasAMessageThat().Contains("] can not be compared to [null]");
        }

        [TestMethod]
        public void IsNotSameCollectionFailsIfSame()
        {
            var testCollection = new List<string>();
            Verify.TheExpectedException(Verify.FailExceptionType).IsThrownWhen(
                () => Verify.That(testCollection).IsACollectionThat().IsNotTheSameCollectionAs(testCollection)
            ).AndHasAMessageThat().Contains("] should not be the same collection as");
        }

        #endregion
    }
}
