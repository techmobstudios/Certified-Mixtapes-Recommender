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

/// <p>Tests {@link CachingRecommender}.</p> 
public sealed class CachingRecommenderTest : TasteTestCase {

  [Test]
  public void testRecommender() {
    var mockRecommender = new MockRecommender(0);

    IRecommender cachingRecommender = new CachingRecommender(mockRecommender);
    cachingRecommender.Recommend(1, 1);
    Assert.AreEqual(1, mockRecommender.recommendCount);
    cachingRecommender.Recommend(2, 1);
	Assert.AreEqual(2, mockRecommender.recommendCount);
    cachingRecommender.Recommend(1, 1);
	Assert.AreEqual(2, mockRecommender.recommendCount);
    cachingRecommender.Recommend(2, 1);
	Assert.AreEqual(2, mockRecommender.recommendCount);
    cachingRecommender.Refresh(null);
    cachingRecommender.Recommend(1, 1);
	Assert.AreEqual(3, mockRecommender.recommendCount);
    cachingRecommender.Recommend(2, 1);
	Assert.AreEqual(4, mockRecommender.recommendCount);
    cachingRecommender.Recommend(3, 1);
	Assert.AreEqual(5, mockRecommender.recommendCount);

    // Results from this recommend() method can be cached...
    IDRescorer rescorer = NullRescorer.getItemInstance();
    cachingRecommender.Refresh(null);
    cachingRecommender.Recommend(1, 1, rescorer);
	Assert.AreEqual(6, mockRecommender.recommendCount);
    cachingRecommender.Recommend(2, 1, rescorer);
	Assert.AreEqual(7, mockRecommender.recommendCount);
    cachingRecommender.Recommend(1, 1, rescorer);
	Assert.AreEqual(7, mockRecommender.recommendCount);
    cachingRecommender.Recommend(2, 1, rescorer);
	Assert.AreEqual(7, mockRecommender.recommendCount);

    // until you switch Rescorers
    cachingRecommender.Recommend(1, 1, null);
	Assert.AreEqual(8, mockRecommender.recommendCount);
    cachingRecommender.Recommend(2, 1, null);
	Assert.AreEqual(9, mockRecommender.recommendCount);

    cachingRecommender.Refresh(null);
    cachingRecommender.EstimatePreference(1, 1);
	Assert.AreEqual(10, mockRecommender.recommendCount);
    cachingRecommender.EstimatePreference(1, 2);
	Assert.AreEqual(11, mockRecommender.recommendCount);
    cachingRecommender.EstimatePreference(1, 2);
	Assert.AreEqual(11, mockRecommender.recommendCount);
  }

}

}