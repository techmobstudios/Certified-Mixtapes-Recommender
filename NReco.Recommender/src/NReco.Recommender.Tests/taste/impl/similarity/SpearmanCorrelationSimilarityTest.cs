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

/// <p>Tests {@link SpearmanCorrelationSimilarity}.</p> 
public sealed class SpearmanCorrelationSimilarityTest : SimilarityTestCase {

  [Test]
  public void testFullCorrelation1() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{1.0, 2.0, 3.0},
                    new double?[]{1.0, 2.0, 3.0},
            });
    double correlation = new SpearmanCorrelationSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(1.0, correlation);
  }

  [Test]
  public void testFullCorrelation2() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                   new double?[] {1.0, 2.0, 3.0},
                   new double?[] {4.0, 5.0, 6.0},
            });
    double correlation = new SpearmanCorrelationSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(1.0, correlation);
  }

  [Test]
  public void testAnticorrelation() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{1.0, 2.0, 3.0},
                    new double?[]{3.0, 2.0, 1.0},
            });
    double correlation = new SpearmanCorrelationSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(-1.0, correlation);
  }

  [Test]
  public void testSimple() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{1.0, 2.0, 3.0},
                    new double?[]{2.0, 3.0, 1.0},
            });
    double correlation = new SpearmanCorrelationSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(-0.5, correlation);
  }

  [Test]
  public void testRefresh() {
    // Make sure this doesn't throw an exception
    new SpearmanCorrelationSimilarity(getDataModel()).Refresh(null);
  }

}

}