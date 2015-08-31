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
using NReco.CF.Taste.Impl.Common;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Recommender.SVD {


public class FilePersistenceStrategyTest : TasteTestCase {

  [Test]
  public void persistAndLoad() {
    FastByIDMap<int?> userIDMapping = new FastByIDMap<int?>();
    FastByIDMap<int?> itemIDMapping = new FastByIDMap<int?>();

    userIDMapping.Put(123, 0);
    userIDMapping.Put(456, 1);

    itemIDMapping.Put(12, 0);
    itemIDMapping.Put(34, 1);

    double[][] userFeatures = new double[][] { new double[] { 0.1, 0.2, 0.3 }, new double[] { 0.4, 0.5, 0.6 } };
    double[][] itemFeatures = new double[][] { new double[] { 0.7, 0.8, 0.9 }, new double[] { 1.0, 1.1, 1.2 } };

    Factorization original = new Factorization(userIDMapping, itemIDMapping, userFeatures, itemFeatures);
    var storage = Path.Combine( Path.GetTempPath(), "storage.bin");
	try {
		IPersistenceStrategy persistenceStrategy = new FilePersistenceStrategy(storage);

		Assert.IsNull(persistenceStrategy.Load());

		persistenceStrategy.MaybePersist(original);
		Factorization clone = persistenceStrategy.Load();

		Assert.True(original.Equals( clone ) );
	} finally {
		if (File.Exists(storage))
			try { File.Delete(storage); } catch { }
	}
  }
}

}