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

/// <p>Tests {@link FullRunningAverage}.</p> 
public sealed class RunningAverageTest : TasteTestCase {

  [Test]
  public void testFull() {
    IRunningAverage runningAverage = new FullRunningAverage();

    Assert.AreEqual(0, runningAverage.GetCount());
    Assert.True(Double.IsNaN(runningAverage.GetAverage()));
    runningAverage.AddDatum(1.0);
    Assert.AreEqual(1, runningAverage.GetCount());
    Assert.AreEqual(1.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.AddDatum(1.0);
    Assert.AreEqual(2, runningAverage.GetCount());
    Assert.AreEqual(1.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.AddDatum(4.0);
    Assert.AreEqual(3, runningAverage.GetCount());
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.AddDatum(-4.0);
    Assert.AreEqual(4, runningAverage.GetCount());
    Assert.AreEqual(0.5, runningAverage.GetAverage(), EPSILON);

    runningAverage.RemoveDatum(-4.0);
    Assert.AreEqual(3, runningAverage.GetCount());
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.RemoveDatum(4.0);
    Assert.AreEqual(2, runningAverage.GetCount());
    Assert.AreEqual(1.0, runningAverage.GetAverage(), EPSILON);

    runningAverage.ChangeDatum(0.0);
    Assert.AreEqual(2, runningAverage.GetCount());
    Assert.AreEqual(1.0, runningAverage.GetAverage(), EPSILON);
    runningAverage.ChangeDatum(2.0);
    Assert.AreEqual(2, runningAverage.GetCount());
    Assert.AreEqual(2.0, runningAverage.GetAverage(), EPSILON);
  }
  
  [Test]
  public void testCopyConstructor() {
    IRunningAverage runningAverage = new FullRunningAverage();

    runningAverage.AddDatum(1.0);
    runningAverage.AddDatum(1.0);
    Assert.AreEqual(2, runningAverage.GetCount());
    Assert.AreEqual(1.0, runningAverage.GetAverage(), EPSILON);

    IRunningAverage copy = new FullRunningAverage(runningAverage.GetCount(), runningAverage.GetAverage());
    Assert.AreEqual(2, copy.GetCount());
    Assert.AreEqual(1.0, copy.GetAverage(), EPSILON);
    
  }

}

}