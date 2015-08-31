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
using NReco.CF.Taste.Recommender;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Recommender {

/// <p>Tests {@link NullRescorer}.</p> 
public sealed class NullRescorerTest : TasteTestCase {

  [Test]
  public void testItemRescorer() {
    IDRescorer rescorer = NullRescorer.getItemInstance();
    Assert.NotNull(rescorer);
    Assert.AreEqual(1.0, rescorer.rescore(1L, 1.0), EPSILON);
    Assert.AreEqual(1.0, rescorer.rescore(0L, 1.0), EPSILON);
    Assert.AreEqual(0.0, rescorer.rescore(1L, 0.0), EPSILON);
    Assert.True(Double.IsNaN(rescorer.rescore(1L, Double.NaN)));
  }

  [Test]
  public void testUserRescorer() {
    IDRescorer rescorer = NullRescorer.getUserInstance();
    Assert.NotNull(rescorer);
    Assert.AreEqual(1.0, rescorer.rescore(1L, 1.0), EPSILON);
    Assert.AreEqual(1.0, rescorer.rescore(0L, 1.0), EPSILON);
    Assert.AreEqual(0.0, rescorer.rescore(1L, 0.0), EPSILON);
    Assert.True(Double.IsNaN(rescorer.rescore(1L, Double.NaN)));
  }

}

}