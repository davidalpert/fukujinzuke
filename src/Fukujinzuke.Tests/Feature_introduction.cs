﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fukujinzuke.ObjectModel;
using NUnit.Framework;

namespace Fukujinzuke.Tests
{
    /// <summary>
    /// Every .feature file conventionally consists of a single feature. 
    /// </summary>
    [TestFixture]
    public class Feature_introduction
    {
        /// <summary>
        /// A line starting with the keyword Feature followed by free indented text starts a feature.
        /// </summary>
        [Test]
        public void Feature_starts_with_name_and_description()
        {
            var input = @"
Feature: Serve coffee
    Coffee should not be served until paid for
    Coffee should not be served until the button has been pressed
    If there is no coffee left then money should be refunded
";
            var result = FeatureFileParser.Parse(input);

            Assert.IsInstanceOf<Feature>(result);

            Assert.AreEqual("Serve coffee", result.Title);
            Assert.AreEqual(@"Coffee should not be served until paid for
Coffee should not be served until the button has been pressed
If there is no coffee left then money should be refunded".NormalizeLineEndings(), result.Description);
        }
    }
    public static class StringExtensions
    {
        public static string NormalizeLineEndings(this string input)
        {
            return input.Replace("\r", "");
        }
    }
}
