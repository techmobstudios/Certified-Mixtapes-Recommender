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
using NReco.CF.Taste.Impl.Common;
using NReco.CF.Taste.Impl.Model;
using NReco.CF.Taste.Model;
using NReco.CF.Taste.Recommender;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Recommender {


 /// Tests {@link SamplingCandidateItemsStrategy}
public sealed class SamplingCandidateItemsStrategyTest : TasteTestCase {

  [Test]
  public void testStrategy() {
    List<IPreference> prefsOfUser123 = new List<IPreference>();
    prefsOfUser123.Add(new GenericPreference(123L, 1L, 1.0f));

    List<IPreference> prefsOfUser456 = new List<IPreference>();
    prefsOfUser456.Add(new GenericPreference(456L, 1L, 1.0f));
    prefsOfUser456.Add(new GenericPreference(456L, 2L, 1.0f));

	List<IPreference> prefsOfUser789 = new List<IPreference>();
    prefsOfUser789.Add(new GenericPreference(789L, 1L, 0.5f));
    prefsOfUser789.Add(new GenericPreference(789L, 3L, 1.0f));

    IPreferenceArray prefArrayOfUser123 = new GenericUserPreferenceArray(prefsOfUser123);

    FastByIDMap<IPreferenceArray> userData = new FastByIDMap<IPreferenceArray>();
    userData.Put(123L, prefArrayOfUser123);
    userData.Put(456L, new GenericUserPreferenceArray(prefsOfUser456));
    userData.Put(789L, new GenericUserPreferenceArray(prefsOfUser789));

    IDataModel dataModel = new GenericDataModel(userData);

    ICandidateItemsStrategy strategy =
        new SamplingCandidateItemsStrategy(1, 1, 1, dataModel.GetNumUsers(), dataModel.GetNumItems());

    FastIDSet candidateItems = strategy.GetCandidateItems(123L, prefArrayOfUser123, dataModel);
	Assert.True(candidateItems.Count() <= 1);
    Assert.False(candidateItems.Contains(1L));
  }
}

}