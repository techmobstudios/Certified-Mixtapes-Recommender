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

namespace NReco.CF.Taste.Impl.Common {

 /// <p>Tests {@link WeightedRunningAverage} and {@link WeightedRunningAverageAndStdDev}.</p>
public sealed class WeightedRunningAverageTest : TasteTestCase {

  [Test]
  public void testWeighted() {

    WeightedRunningAverage runningAverage = new WeightedRunningAverage();

    Assert.AreEqual(0, runningAverage.GetCount());
    Assert.True(Double.IsNaN(runningAverage.GetAverage()));
    runningAverage.AddDatum(1.0, 2.0);
    Assert.AreEqual(1.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.AddDatum(1.0);
    Assert.AreEqual(1.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.AddDatum(8.0, 0.5);
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.AddDatum(-4.0);
    Assert.AreEqual(2.0/3.0, runningAverage.GetAverage(), EPSILON);

    runningAverage.RemoveDatum(-4.0);
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.RemoveDatum(2.0, 2.0);
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);

    runningAverage.ChangeDatum(0.0);
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.ChangeDatum(4.0, 0.5);
    Assert.AreEqual(5.0/1.5, runningAverage.GetAverage(), EPSILON);
  }

  [Test]
  public void testWeightedAndStdDev() {

    WeightedRunningAverageAndStdDev runningAverage = new WeightedRunningAverageAndStdDev();

    Assert.AreEqual(0, runningAverage.GetCount());
    Assert.True(Double.IsNaN(runningAverage.GetAverage()));
    Assert.True(Double.IsNaN(runningAverage.GetStandardDeviation()));

    runningAverage.AddDatum(1.0);
    Assert.AreEqual(1.0, runningAverage.GetAverage(), EPSILON);
    Assert.True(Double.IsNaN(runningAverage.GetStandardDeviation()));
    runningAverage.AddDatum(1.0, 2.0);
    Assert.AreEqual(1.0, runningAverage.GetAverage(), EPSILON);
    Assert.AreEqual(0.0, runningAverage.GetStandardDeviation(), EPSILON);
    runningAverage.AddDatum(8.0, 0.5);
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);
    Assert.AreEqual(Math.Sqrt(10.5), runningAverage.GetStandardDeviation(), EPSILON);
    runningAverage.AddDatum(-4.0);
    Assert.AreEqual(2.0/3.0, runningAverage.GetAverage(), EPSILON);
    Assert.AreEqual(Math.Sqrt(15.75), runningAverage.GetStandardDeviation(), EPSILON);

    runningAverage.RemoveDatum(-4.0);
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);
    Assert.AreEqual(Math.Sqrt(10.5), runningAverage.GetStandardDeviation(), EPSILON);
    runningAverage.RemoveDatum(2.0, 2.0);
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);
    Assert.AreEqual(Math.Sqrt(31.5), runningAverage.GetStandardDeviation(), EPSILON);
  }

}

}