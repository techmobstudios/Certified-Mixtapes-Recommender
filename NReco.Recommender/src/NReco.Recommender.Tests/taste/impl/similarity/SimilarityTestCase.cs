/*
 *  Copyright 2013-2014 Vitalii Fedorchenko (nrecosite.com)
 *
 *  Licensed under Source Code Licence (see LICENSE file).
 *  
 *  Parts of this code are based on Apache Mahout ("Taste") that was licensed under the
 *  Apache 2.0 License (see http://www.apache.org/licenses/LICENSE-2.0).
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS 
 *  OF ANY KIND, either express or implied.
 */

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NReco.CF.Taste.Impl;

using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Similarity {

public abstract class SimilarityTestCase : TasteTestCase {

  public static void assertCorrelationEquals(double expected, double actual) {
    if (Double.IsNaN(expected)) {
		Assert.True(Double.IsNaN(actual), "Correlation is not NaN");
    } else {
		Assert.True(!Double.IsNaN(actual), "Correlation is NaN");
		Assert.True(actual <= 1.0, "Correlation > 1.0");
		Assert.True(actual >= -1.0, "Correlation < -1.0");
      Assert.AreEqual(expected, actual, EPSILON);
    }
  }

}

}