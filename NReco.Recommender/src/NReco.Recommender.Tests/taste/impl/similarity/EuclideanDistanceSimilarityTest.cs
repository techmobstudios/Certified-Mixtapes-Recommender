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
using NReco.CF.Taste.Model;
using NReco.CF.Taste.Similarity;
using NUnit.Framework;

namespace NReco.CF.Taste.Impl.Similarity {

/// <p>Tests {@link EuclideanDistanceSimilarity}.</p> 
public sealed class EuclideanDistanceSimilarityTest : SimilarityTestCase {

  [Test]
  public void testFullCorrelation1() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{3.0, -2.0},
                    new double?[]{3.0, -2.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(1.0, correlation);
  }

  [Test]
  public void testFullCorrelation1Weighted() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{3.0, -2.0},
                    new double?[]{3.0, -2.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel, Weighting.WEIGHTED).UserSimilarity(1, 2);
    assertCorrelationEquals(1.0, correlation);
  }

  [Test]
  public void testFullCorrelation2() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{3.0, 3.0},
                    new double?[]{3.0, 3.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel).UserSimilarity(1, 2);
    Assert.AreEqual(1.0, correlation, EPSILON);
  }

  [Test]
  public void testNoCorrelation1() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{3.0, -2.0},
                    new double?[]{-3.0, 2.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(0.1639607805437114, correlation);
  }

  [Test]
  public void testNoCorrelation1Weighted() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{3.0, -2.0},
                    new double?[]{-3.0, 2.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel, Weighting.WEIGHTED).UserSimilarity(1, 2);
    assertCorrelationEquals(0.7213202601812372, correlation);
  }

  [Test]
  public void testNoCorrelation2() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{null, 1.0, null},
                    new double?[]{null, null, 1.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel).UserSimilarity(1, 2);
    Assert.True(Double.IsNaN(correlation));
  }

  [Test]
  public void testNoCorrelation3() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{90.0, 80.0, 70.0},
                    new double?[]{70.0, 80.0, 90.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(0.05770363219029305, correlation);
  }

  [Test]
  public void testSimple() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{1.0, 2.0, 3.0},
                    new double?[]{2.0, 5.0, 6.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel).UserSimilarity(1, 2);
    assertCorrelationEquals(0.2843646522044218, correlation);
  }

  [Test]
  public void testSimpleWeighted() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{1.0, 2.0, 3.0},
                    new double?[]{2.0, 5.0, 6.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel, Weighting.WEIGHTED).UserSimilarity(1, 2);
    assertCorrelationEquals(0.8210911630511055, correlation);
  }

  [Test]
  public void testFullItemCorrelation1() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{3.0, 3.0},
                    new double?[]{-2.0, -2.0},
            });
    double correlation =
        new EuclideanDistanceSimilarity(dataModel).ItemSimilarity(0, 1);
    assertCorrelationEquals(1.0, correlation);
  }

  [Test]
  public void testFullItemCorrelation2() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{3.0, 3.0},
                    new double?[]{3.0, 3.0},
            });
    double correlation =
        new EuclideanDistanceSimilarity(dataModel).ItemSimilarity(0, 1);
    Assert.AreEqual(1.0, correlation, EPSILON);
  }

  [Test]
  public void testNoItemCorrelation1() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{3.0, -3.0},
                    new double?[]{-2.0, 2.0},
            });
    double correlation =
        new EuclideanDistanceSimilarity(dataModel).ItemSimilarity(0, 1);
    assertCorrelationEquals(0.1639607805437114, correlation);
  }

  [Test]
  public void testNoItemCorrelation2() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2},
            new Double?[][] {
                    new double?[]{null, 1.0, null},
                    new double?[]{null, null, 1.0},
            });
    double correlation = new EuclideanDistanceSimilarity(dataModel).ItemSimilarity(1, 2);
    Assert.True(Double.IsNaN(correlation));
  }

  [Test]
  public void testNoItemCorrelation3() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2, 3},
            new Double?[][] {
                    new double?[]{90.0, 70.0},
                    new double?[]{80.0, 80.0},
                    new double?[]{70.0, 90.0},
            });
    double correlation =
        new EuclideanDistanceSimilarity(dataModel).ItemSimilarity(0, 1);
    assertCorrelationEquals(0.05770363219029305, correlation);
  }

  [Test]
  public void testSimpleItem() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2, 3},
            new Double?[][] {
                    new double?[]{1.0, 2.0},
                    new double?[]{2.0, 5.0},
                    new double?[]{3.0, 6.0},
            });
    double correlation =
        new EuclideanDistanceSimilarity(dataModel).ItemSimilarity(0, 1);
    assertCorrelationEquals(0.2843646522044218, correlation);
  }

  [Test]
  public void testSimpleItemWeighted() {
    IDataModel dataModel = getDataModel(
            new long[] {1, 2, 3},
            new Double?[][] {
                    new double?[]{1.0, 2.0},
                    new double?[]{2.0, 5.0},
                    new double?[]{3.0, 6.0},
            });
    IItemSimilarity itemSimilarity = new EuclideanDistanceSimilarity(dataModel, Weighting.WEIGHTED);
    double correlation = itemSimilarity.ItemSimilarity(0, 1);
    assertCorrelationEquals(0.8210911630511055, correlation);
  }

  [Test]
  public void testRefresh() {
    // Make sure this doesn't throw an exception
    new EuclideanDistanceSimilarity(getDataModel()).Refresh(null);
  }

}
}