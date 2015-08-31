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
using NReco.CF.Taste.Model;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Neighborhood {

/// <p>Tests {@link NearestNUserNeighborhood}.</p> 
public sealed class NearestNNeighborhoodTest : TasteTestCase {

  [Test]
  public void testNeighborhood() {
    IDataModel dataModel = getDataModel();
	  
    long[] neighborhood =
        new NearestNUserNeighborhood(1, new DummySimilarity(dataModel), dataModel).GetUserNeighborhood(1);
    Assert.NotNull(neighborhood);
    Assert.AreEqual(1, neighborhood.Length);
    Assert.True(arrayContains(neighborhood, 2));

    long[] neighborhood2 =
        new NearestNUserNeighborhood(2, new DummySimilarity(dataModel), dataModel).GetUserNeighborhood(2);
    Assert.NotNull(neighborhood2);
    Assert.AreEqual(2, neighborhood2.Length);
    Assert.True(arrayContains(neighborhood2, 1));
    Assert.True(arrayContains(neighborhood2, 3));

    long[] neighborhood3 =
        new NearestNUserNeighborhood(4, new DummySimilarity(dataModel), dataModel).GetUserNeighborhood(4);
    Assert.NotNull(neighborhood3);
    Assert.AreEqual(3, neighborhood3.Length);
    Assert.True(arrayContains(neighborhood3, 1));
    Assert.True(arrayContains(neighborhood3, 2));
    Assert.True(arrayContains(neighborhood3, 3));
  }

}

}