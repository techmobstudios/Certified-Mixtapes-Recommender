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
using NReco.CF.Taste.Model;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Similarity {

/// <p>Tests {@link LogLikelihoodSimilarity}.</p> 
public sealed class LogLikelihoodSimilarityTest : SimilarityTestCase {

  [Test]
  public void testCorrelation() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2, 3, 4, 5},
            new Double?[][] {
                    new double?[]{1.0, 1.0},
                    new double?[]{1.0, null, 1.0},
                    new double?[]{null, null, 1.0, 1.0, 1.0},
                    new double?[]{1.0, 1.0, 1.0, 1.0, 1.0},
                    new double?[]{null, 1.0, 1.0, 1.0, 1.0},
            });

    LogLikelihoodSimilarity similarity = new LogLikelihoodSimilarity(dataModel);

    assertCorrelationEquals(0.12160727029227925, similarity.ItemSimilarity(1, 0));
    assertCorrelationEquals(0.12160727029227925, similarity.ItemSimilarity(0, 1));

    assertCorrelationEquals(0.5423213660693732, similarity.ItemSimilarity(1, 2));
    assertCorrelationEquals(0.5423213660693732, similarity.ItemSimilarity(2, 1));

    assertCorrelationEquals(0.6905400104897509, similarity.ItemSimilarity(2, 3));
    assertCorrelationEquals(0.6905400104897509, similarity.ItemSimilarity(3, 2));

    assertCorrelationEquals(0.8706358464330881, similarity.ItemSimilarity(3, 4));
    assertCorrelationEquals(0.8706358464330881, similarity.ItemSimilarity(4, 3));
  }

  [Test]
  public void testNoSimilarity() {

    IDataModel dataModel = getDataModel(
        new long[] {1, 2, 3, 4},
        new Double?[][] {
                new double?[]{1.0, null, 1.0, 1.0},
                new double?[]{1.0, null, 1.0, 1.0},
                new double?[]{null, 1.0, 1.0, 1.0},
                new double?[]{null, 1.0, 1.0, 1.0},
        });

    LogLikelihoodSimilarity similarity = new LogLikelihoodSimilarity(dataModel);

    assertCorrelationEquals(Double.NaN, similarity.ItemSimilarity(1, 0));
    assertCorrelationEquals(Double.NaN, similarity.ItemSimilarity(0, 1));

    assertCorrelationEquals(0.0, similarity.ItemSimilarity(2, 3));
    assertCorrelationEquals(0.0, similarity.ItemSimilarity(3, 2));
  }

  [Test]
  public void testRefresh() {
    // Make sure this doesn't throw an exception
    new LogLikelihoodSimilarity(getDataModel()).Refresh(null);
  }

}
}