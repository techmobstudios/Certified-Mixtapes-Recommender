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


public sealed class ItemUserAverageRecommenderTest : TasteTestCase {

  [Test]
  public void testRecommender() {
    IRecommender recommender = new ItemUserAverageRecommender(getDataModel());
    IList<IRecommendedItem> recommended = recommender.Recommend(1, 1);
    Assert.NotNull(recommended);
    Assert.AreEqual(1, recommended.Count);
    IRecommendedItem firstRecommended = recommended[0];
    Assert.AreEqual(2, firstRecommended.GetItemID());
    Assert.AreEqual(0.35151517f, firstRecommended.GetValue(), EPSILON);
    recommender.Refresh(null);
    Assert.AreEqual(2, firstRecommended.GetItemID());
    Assert.AreEqual(0.35151517f, firstRecommended.GetValue(), EPSILON);
  }

}
}