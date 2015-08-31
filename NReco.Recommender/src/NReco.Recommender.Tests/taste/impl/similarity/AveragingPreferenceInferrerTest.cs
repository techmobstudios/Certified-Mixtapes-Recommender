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
using NReco.CF.Taste.Common;
using NReco.CF.Taste.Impl;
using NReco.CF.Taste.Model;
using NReco.CF.Taste.Similarity;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Similarity {

/// <p>Tests {@link AveragingPreferenceInferrer}.</p> 
public sealed class AveragingPreferenceInferrerTest : TasteTestCase {

  [Test]
  public void testInferrer() {
    IDataModel model = getDataModel(new long[] {1}, new Double?[][] { new double?[] {3.0,-2.0,5.0} });
    IPreferenceInferrer inferrer = new AveragingPreferenceInferrer(model);
    double inferred = inferrer.InferPreference(1, 3);
    Assert.AreEqual(2.0, inferred, EPSILON);
  }

}

}