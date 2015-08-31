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
using NReco.CF.Taste.Recommender;

namespace NReco.CF.Taste.Impl.Recommender {



public sealed class MockRecommender : IRecommender {

  public int recommendCount;

  public MockRecommender(int recommendCount) {
    this.recommendCount = recommendCount;
  }

  public IList<IRecommendedItem> Recommend(long userID, int howMany) {
	  lock (this) {
		  recommendCount++;
	  }
	return new List<IRecommendedItem>() {
        new GenericRecommendedItem(1, 1.0f) };
  }

  public IList<IRecommendedItem> Recommend(long userID, int howMany, IDRescorer rescorer) {
    return Recommend(userID, howMany);
  }

  public float EstimatePreference(long userID, long itemID) {
	  lock (this) {
		  recommendCount++;
	  }
    return 0.0f;
  }

  public void SetPreference(long userID, long itemID, float value) {
    // do nothing
  }

  public void RemovePreference(long userID, long itemID) {
    // do nothing
  }

  public IDataModel GetDataModel() {
    return TasteTestCase.getDataModel(
            new long[] {1, 2, 3},
            new Double?[][]{
				new double?[]{1.0},
				new double?[]{2.0},
				new double?[]{3.0}
			});
  }

  public void Refresh(IList<IRefreshable> alreadyRefreshed) {
    // do nothing
  }

}

}